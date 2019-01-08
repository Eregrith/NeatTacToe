using NeatTacToe.Game;
using NeatTacToe.Players;
using NeuralNetwork.Genetics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace NeatTacToe
{
    public static class Program
    {
        static readonly string bestDNA = "-0.150388451985663||-0.215599277692156||0.00800633311802478||3.07110785432547||-8.01316500325599E-05||-0.0510379885986991||0.000213633485866914||0.0716126662498744||0.000124404326028656||-0.00150681693817462||-0.00158612311271861||0.000330344362773551||0.136419792616884||-0.000434005309074052||-0.0112189874917233||1.10898886792433||-0.00745158156418951||-0.0253561143191025||0.175422438677974||-0.00422549504087974||-1.70076945615557||0.585056638299451||-0.0035826648039684||0.0107579842514956||0.410564757113277||0.00249378425904649||0.0165405044884962||-0.0136201773595977||-0.0108450304829766||-0.166218809889841||-0.103293153006773||0.032177901450709||-0.0172461277901829||0.00779217677920189||0.581751362885457||-2.16394212801562||-6.78021136013738||0.187210458424425||0.613307781941186||-0.00826322348003256||0.000205644269441194||0.00721734376224744||0.37871985838486||-0.0206598721485181||9.51976234236835E-09||-1.48879407045995||3.40288967468158||0.00482804955799803||-0.584007082079107||-0.213459082712017||0.170298924536758||0.0102456997524273||-0.365801765860082||-1.81044362108147E-08||-0.16218945930529||1.42447845372813||-0.585056638299451||-0.203540642481174||-0.0617562391823288||-0.0229229157517466||0.663561143536236||-0.38978403974849||-3.39438225375168||5.51760235266265||0.000639019751588214||-0.627176235333606||1.23862426114423||0.897464374279971||0.000531512548170662||0.194578343233898||0.499970857466719||-0.0213383735566326||-0.658535017194193||0.216719477407886||-0.585056638299451||0.000213633485866914||0.026944441994677||-0.278916529435034||-0.026944441994677||-1.02614749926375||-0.00149426614985111||0.616892965741677||0.0496364939420515||-0.332595847726563||0.052248941647271||2.06322537240419||0.499970857466719||0.00351008614638213||1.19764678190349||1.81044362108147E-08||5.00983090517351||0.00911442484872578||0.215599277692156||0.0335255044455754||0.215599277692156||-0.00911442484872578||-0.0319166780838618||-0.000672652378533532||1.81044362108147E-08||-0.800554144425498||0.0234892215919971||-0.00459433414474532||-8.08366233666445||-0.583593962877772||-0.00106770291093709||-0.237698182066396||0.0138070920520046||-0.00694440553233602||-0.884222966826308||0.00130620002243193||0.0160234924165955||0.0272541265970907||-6.10620284705203E-05||0.00249378425904649||-0.0286429097155192||-0.0897919780426184||-0.524969376499587||0.0030914593096327||-0.065006568376071||-0.658535017194193||-0.389549061928855||0.0157528621330011||-0.00449543247562974||3.23274515038189||-0.00376954608572194||0.000822926977984432||-0.00708259037258687||-0.00150681693817462||-0.150388451985663||0.000193803216274391||0.0107579842514956||0.000742691578991847||-0.0258914199423417||-0.702108502413243||0.00792792666236485||0.0035574755992149||0.443246333649604||0.443246333649604||-0.472808907551633||0.0102456997524273||-0.103293153006773||0.194578343233898||0.00512493198211732||0.194578343233898||-0.783113732199406||-0.000213099389800705||-1.81044362108147E-08||-0.00249700416373978||-0.332595847726563||-0.00101685996135691||0.000205644269441194||-0.000224315149973421||-0.00826322348003256||-0.000368855755420423||0.167068996804233||-0.0035826648039684||-0.00869813008812879||0.167068996804233||0.00101940854164968||-0.556288288113886||0.000991383186676881||0.00800633311802478||-0.0682568936951198||-0.00288884015466636||0.557474873753951";
        static readonly string PopulationFile = "C:/Temp/neattactoe_{0}.pop";
        static readonly int GenToLoad = 4000;
        static readonly string PopulationFileToLoad = String.Format(PopulationFile, GenToLoad);

        static void Main(string[] args)
        {
            PlayVSHuman();

            //MatchReplay();

            //DoGenetics();
            //GeneOutput.CheckMating();
            //CreatePopulationImages();
            Console.ReadLine();
        }

        private static void MatchReplay()
        {
            int inputCount = 9;
            int outputCount = 9;
            int depth = 3;
            int hiddenNeuronsPerLayer = 5;

            NeuralPlayer champion = MakeNeuralPlayer(inputCount, outputCount, depth, hiddenNeuronsPerLayer, new DNA(bestDNA));
            OptimalPlayer p2 = new OptimalPlayer(SquareTypes.O);
            champion.SquareType = SquareTypes.X;
            TicTacToeGame.PlayGameToEnd(champion, p2);
        }

        private static void CreatePopulationImages()
        {
            for (int i = GenToLoad+1; i < GenToLoad+2000; i += 50)
            {
                CreateImageForGen(i);
            }
            CreateImageForGen(GenToLoad+2000);
        }

        private static void CreateImageForGen(int i)
        {
            PopulationManager pop = new PopulationManager(new RandomEnvironment());
            if (File.Exists(String.Format(PopulationFile, i)))
            {
                FileStream stream = new FileStream(String.Format(PopulationFile, i), FileMode.Open);
                pop.LoadFromStream(stream);
                Console.WriteLine($"Loaded from file {String.Format(PopulationFile, i)}");
                GeneOutput.CreatePopImage($"neattactoe_{i}.bmp", pop);
            }
            else
            {
                Console.WriteLine($"File {String.Format(PopulationFile, i)} not found.");
            }
        }

        private static void PlayVSHuman()
        {
            int inputCount = 9;
            int outputCount = 9;
            int depth = 3;
            int hiddenNeuronsPerLayer = 5;
            TicTacToeGame game = new TicTacToeGame();

            NeuralPlayer champion = MakeNeuralPlayer(inputCount, outputCount, depth, hiddenNeuronsPerLayer, new DNA(bestDNA));
            champion.SquareType = SquareTypes.X;
            SquareTypes winner = SquareTypes.N;

            for (int moveNum = 0; moveNum < 9 && winner == SquareTypes.N; moveNum++)
            {
                Console.WriteLine($"+- Turn {moveNum + 1} -+");
                TicTacToeGame.DisplayBoard(game.Board);

                SquareTypes curSquareType;
                Move move;

                if (moveNum % 2 == 1)
                {
                    curSquareType = SquareTypes.O;
                    move = AskHumanForMove();
                }
                else
                {
                    curSquareType = SquareTypes.X;
                    move = champion.GetMove(game.Board);
                }

                if (game.IsEmpty(move.X, move.Y))
                    game.Board[move.X, move.Y] = curSquareType;
                else
                    Console.WriteLine($"Player {curSquareType.ToString()} tried to use non-empty space [{move.X}, {move.Y}]");

                if (moveNum > 3)
                    winner = game.GetWinner();
            }

            Console.WriteLine($"The winner is {winner.ToString()}");
        }

        private static Move AskHumanForMove()
        {
            Console.WriteLine("Where to put your X ? (format: x y)");
            string line = Console.ReadLine();
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            Move move = new Move(x, y);
            
            return move;
        }

        private static void DoGenetics()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (FileStream fs = File.OpenWrite("C:/Temp/generations.csv"))
            using (StreamWriter saveFileWriter = new StreamWriter(fs))
                RunExperiment(saveFileWriter);

            sw.Stop();
            Console.WriteLine($"Experiment finished in {sw.Elapsed.ToString(@"hh\:mm\:ss")}");
        }

        private static void RunExperiment(StreamWriter saveFileWriter)
        {
            int inputCount = 9;
            int outputCount = 9;
            int depth = 3;
            int hiddenNeuronsPerLayer = 5;
            PopulationManager pop = GeneratePopulation(inputCount, outputCount, depth, hiddenNeuronsPerLayer);
            Dictionary<int, List<double>> fitnesses = new Dictionary<int, List<double>>();
            DNA bestIndividual = new DNA(bestDNA);

            const int genCount = 2000;
            using (ProgressBar progress = new ProgressBar())
            {
                Console.WriteLine("Running genetics...");
                for (int gen = 0; gen < genCount; gen++)
                {
                    progress.SetLabel($"gen {gen+GenToLoad}/{genCount+GenToLoad}");
                    progress.Report((double)gen / genCount);
                    foreach (DNA individual in pop.Population)
                    {
                        NeuralPlayer p1 = MakeNeuralPlayer(inputCount, outputCount, depth, hiddenNeuronsPerLayer, individual);
                        NeuralPlayer p2 = MakeNeuralPlayer(inputCount, outputCount, depth, hiddenNeuronsPerLayer, bestIndividual);

                        p1.SquareType = SquareTypes.X;
                        p2.SquareType = SquareTypes.O;
                        for (int g = 0; g < 50; g++)
                        {
                            (SquareTypes winner, int moveNum) = TicTacToeGame.PlayGameToEnd(p1, p2);
                            if (winner == SquareTypes.X)
                                individual.Fitness += 50f / moveNum;
                            else if (winner == SquareTypes.N)
                                individual.Fitness += 10f / moveNum;
                        }
                        p2.SquareType = SquareTypes.X;
                        p1.SquareType = SquareTypes.O;
                        for (int g = 0; g < 50; g++)
                        {
                            (SquareTypes winner, int moveNum) = TicTacToeGame.PlayGameToEnd(p2, p1);
                            if (winner == SquareTypes.O)
                                individual.Fitness += 50f / moveNum;
                            else if (winner == SquareTypes.N)
                                individual.Fitness += 10f / moveNum;
                        }
                    }
                    if (gen % 50 == 0)
                        SaveGeneration(gen+1+GenToLoad, pop);
                    int dnaCounter = 0;
                    pop.Population.OrderByDescending(i => i.Fitness).ToList().ForEach(dna =>
                    {
                        if (!fitnesses.ContainsKey(dnaCounter))
                            fitnesses[dnaCounter] = new List<double>();
                        fitnesses[dnaCounter].Add(dna.Fitness);
                        dnaCounter++;
                    });
                    var thisBest = pop.Population.OrderByDescending(i => i.Fitness).First();
                    if (thisBest.Fitness > bestIndividual.Fitness)
                        bestIndividual = thisBest;
                    pop.NextGeneration();
                }
            }
            Console.WriteLine(" Done.");

            SaveGeneration(genCount + GenToLoad, pop);

            foreach (KeyValuePair<int, List<double>> fitnessList in fitnesses)
            {
                saveFileWriter.WriteLine($"{fitnessList.Key},{String.Join(',', fitnessList.Value)}");
            }

            saveFileWriter.WriteLine("");
            saveFileWriter.WriteLine("Best final dna:");
            saveFileWriter.WriteLine(String.Join("||", pop.Population.OrderByDescending(i => i.Fitness).First().Genes));
        }

        private static void SaveGeneration(int generation, PopulationManager pop)
        {
            FileStream stream;
            if (!File.Exists(String.Format(PopulationFile, generation)))
                stream = File.Create(String.Format(PopulationFile, generation));
            else
                stream = File.OpenWrite(String.Format(PopulationFile, generation));
            pop.WriteToStream(stream);
            stream.Dispose();
        }

        private static NeuralPlayer MakeNeuralPlayer(int inputCount, int outputCount, int depth, int hiddenNeuronsPerLayer, DNA individual)
        {
            NeuralNetwork.NeuralNetwork network = new NeuralNetwork.NeuralNetwork();

            network.BuildFromDNA($"{inputCount}||{outputCount}||{depth}||{hiddenNeuronsPerLayer}||{individual.ToString()}");

            NeuralPlayer p2 = new NeuralPlayer(network, SquareTypes.O);
            return p2;
        }

        private static PopulationManager GeneratePopulation(int inputCount, int outputCount, int depth, int hiddenNeuronsPerLayer)
        {
            PopulationManager pop = new PopulationManager(new RandomEnvironment());
            if (File.Exists(PopulationFileToLoad))
            {
                FileStream stream = new FileStream(PopulationFileToLoad, FileMode.Open);
                pop.LoadFromStream(stream);
                Console.WriteLine($"Loaded from file {PopulationFileToLoad}");
            }
            else
            {
                Console.WriteLine($"File {PopulationFileToLoad} not found, generating population at random");
                pop.GeneratePopulation(30, hiddenNeuronsPerLayer * (inputCount + depth * hiddenNeuronsPerLayer + outputCount));
            }
            return pop;
        }
    }
}
