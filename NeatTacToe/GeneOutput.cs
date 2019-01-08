using NeuralNetwork.Genetics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace NeatTacToe
{
    class GeneOutput
    {
        private static void CheckMating()
        {
            DNA dad = new DNA("0.45695532491041||-0.311733064498841||-0.237342134538071||-0.811953923977963||-0.459076786925547||-0.687220794215915||0.608218634972302||-0.365262258737814||-0.156727033677608||-0.623719431877542||-0.839492259679907||0.0904609222908574||0.82996845575176||0.348706742799672||-0.00564147445153366||-1.14194094823038||0.116377509801779||-0.00878924964804072||0.691948725881273||0.813317517112063||0.298671464402765||0.378774014736617||0.701032877807139||0.588302780153786||-0.539876974983868||0.366230938643992||0.283131011505408||-0.210708110682717||-0.199942497745633||-0.124603742622684||0.381944739658969||-0.0953156690537515||0.0337580997206181||0.268271553994173||-0.641515409587905||1.17898001003098||-0.164013412353659||-0.211296357392072||-0.543918277661755||-2.46267453430508||-0.994374239456737||-0.403102614108078||0.0635423319291846||-0.395277447017274||0.0318448476667805||1.10557551077235||-1.2345682733212||-0.745128965288645||-0.129239718371642||-0.296742740153788||-0.346276317264964||1.33046441433453||0.175138616927087||0.202962051137596||2.2467372341229||-0.356734370508105||-0.341257724455077||0.963738428410583||0.403798576152326||-0.381239134511504||0.728987125253824||-0.782337438155428||-0.358813529477142||-1.5421971319833||-0.134923293714597||-0.645794394374526||0.395770153487036||1.25211063384476||-0.475210364209458||-0.607257587876485||0.489274041857201||0.737625183602182||0.889741852989143||-0.0356402268213655||-0.522652502916564||-0.0449550510858296||0.202917762502898||1.917177200466||-0.879902709992184||1.10549332219974||-0.637625020265779||0.581510123461902||1.14392875650039||0.860684510394308||-0.184321141159503||0.664172283759948||-0.927730871551607||-0.852694801787363||0.351049596817335||-0.809714218403855||0.0339633163437605||-0.465574649707114||0.293918681756819||-0.676199519257146||-0.971473772830394||-1.01446151631991||0.746476290185485||1.79508027470065||-0.154183591703075||-0.24281338025792||-0.0412719729959099||0.361188320048932||-0.31929346530169||0.482620706532982||1.69380160079478||-0.142448065945821||-0.720650523272832||1.72830787504142||0.472040827244072||-0.133520492796954||0.338311055990795||0.269961698361094||-1.38282419436152||-0.341290041315308||-0.921686263961672||0.398210086686065||-0.0232224456587258||-0.0290291860403467||-0.0577370548076458||-0.725063177238219||1.22975910575217||-0.629718809939568||0.901524098161506||-0.269286778506948||1.15497121682977||0.190326390239707||-0.365087274463865||-0.00387618265185634||-0.349549534826919||-1.2468842477967||0.377827057800386||-0.230879743030917||0.00767162472370834||-0.615652519818805||-0.15594435997567||1.07961057438816||0.728681552939063||0.381427264343454||1.02846545126409||-0.0157003478660568||0.979415251295881||-0.229310104067409||0.339750383981628||-0.0520410839231978||0.292355356654768||-1.18750886135384||0.396718392674208||-0.400498609372759||0.469862275422771||0.122883587133011||-0.647298069380787||0.705060207795757||-0.468236453638138||-0.440280387720585||-0.531438693061904||0.532801010499796||0.763565370941251||0.00734297944303824||-0.220436583743158||-0.917846163978001||-0.0599181628599969||1.0233294332896||-1.27747794980839||-0.513764265168835||0.958747471663518");
            DNA mum = new DNA("0.750712735839753||-0.31404153352949||0.694897230228513||1.02190064498125||0.313636528512864||0.420568055221092||-0.519149785020327||0.445102538748034||0.435595784080727||-0.367951598177464||1.04293680990911||-0.00961051817189824||0.98132297212105||-0.141728578079426||-0.733511862864389||-0.027080667666831||-0.243164278066382||-0.307092950835159||0.809248109994594||1.15773392540393||-0.0300671572654659||-0.302305418582054||-0.0148756278916915||0.787682270944626||0.0233190840876312||0.67489404854045||-0.330211087372579||0.711455701069981||0.547622924341779||0.0943148603704964||-1.06765095783648||-0.17105882548711||-0.559038666421415||-0.100118359458224||-0.348840588577464||0.0696517240535172||-0.434823146964626||0.78735884858244||0.396944252103001||-0.437352530012946||-0.429654505504135||-0.449876025673441||0.880813491090214||-0.264812856253386||-0.494084250691268||-0.472496876374842||0.635093849757158||-0.131955028429219||-0.0986414157708111||1.06977370366343||-0.48441134303372||-0.100369288482432||0.0992480762502861||-0.347559883269982||-0.38441978485756||0.0821705876292046||-0.75085810858194||0.328571912719085||-0.244447224335521||1.35210975606252||-0.00670627303723438||0.233151794973984||-0.341366712715941||-0.586655092493142||-0.834845390214584||0.654179080938679||-0.831193390418279||0.205075556902398||-0.101860245572547||0.607239572646593||-0.699066959780045||0.190881575493701||-0.433544991308099||-0.00586981092185695||-1.242493925659||0.765752703808885||0.727819318855358||0.0405624627329697||0.0662780215077276||0.709196256441259||0.50705617612818||-0.544850334342282||0.891069846239375||0.631258960634377||-0.368688251760225||-0.0974792641249465||0.551487890429254||0.611804181695165||-0.414130175130914||0.303665489930885||-0.00554293374173753||-0.82713735393067||-0.142586363685159||-0.938491458469449||0.175104893535021||0.235629006138715||0.427017479195759||0.193264703979711||-0.779384323161135||-0.633682097842185||-0.274136433753843||-0.0854097629755523||0.399682760136685||0.283071882885894||0.738903195882734||0.753980589572126||-1.26052903097266||-0.855885815486586||-0.595863303277004||-0.00570110075681564||-0.0765071075199075||0.581979738915316||-0.469825871786406||-0.0312748491399466||-1.03184318507028||-0.716180511101608||-1.09445549398669||0.3170441371909||-0.69366943109514||-1.03773836091322||0.270628481525562||-1.11767470841545||0.0765097411807637||0.435036449944617||0.520241771596517||-0.34659188700997||-0.341493324798314||0.0332321200959822||0.234890722034726||-0.743747288329704||-0.76320951393133||-0.640276827888794||-0.56652089827693||-0.650285556366431||-0.0775598160508894||0.290742549028311||0.79009731416019||-1.04370387715296||0.98641432578422||0.225949413602889||-0.695539078690351||0.934984288138178||0.211579506498053||-0.00440482050646945||-0.504631870212763||0.30863422861338||-0.121242101799178||0.86921199952642||0.427265676070568||0.550984516226024||1.3703742501725||1.07568490295201||0.23859665758858||-0.256556159270391||1.34247564627329||0.000624924120957068||0.781083830682801||0.458439556859039||-0.246172602391208||-0.465718968505093||0.18006192382486||-0.226238021945107||-1.06983902205879||0.3136530637009||-0.601645394042901");

            dad.Fitness = 1;
            mum.Fitness = 1;
            SaveToPng(dad, "dad.png");
            SaveToPng(mum, "mum.png");
            PopulationManager pop = new PopulationManager(new YesRandomEnvironment());
            pop.Population = new List<DNA> { mum, dad };
            pop.NextGeneration();
            SaveToPng(pop.Population[1], "son.png");
            UpdatePng("son.png", "son_with_marks.png", pop);
        }

        internal static void CreatePopImage(string name, PopulationManager pop)
        {
            int width = 1;
            int height = 1;
            while (width * height < pop.Population.Count)
            {
                if (height < width) height++;
                else width++;
            }
            int geneImageSideLength = 1;
            while (geneImageSideLength * geneImageSideLength < pop.Population[0].Genes.Count) geneImageSideLength++;

            var orderedIndividuals = pop.Population.OrderByDescending(i => i.Fitness).ToList();
            using (Bitmap b = new Bitmap(width * (geneImageSideLength+1) * 10, height * (geneImageSideLength + 1) * 10))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.Black);
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (y * width + x < pop.Population.Count)
                            {
                                DNA individual = orderedIndividuals[y * width + x];
                                DrawDNAAt(g, individual, y * (geneImageSideLength + 1) * 10, x * (geneImageSideLength + 1) * 10, geneImageSideLength);
                            }
                        }
                    }
                }
                b.Save($"C:/Temp/{name}", ImageFormat.Png);
            }
        }

        private static void DrawDNAAt(Graphics g, DNA individual, int yOffset, int xOffset, int geneImageSideLength)
        {
            for (int y = 0; y < geneImageSideLength; y++)
            {
                for (int x = 0; x < geneImageSideLength; x++)
                {
                    if (y * geneImageSideLength + x < individual.Genes.Count)
                    {
                        Rectangle r = new Rectangle(xOffset + (x * 10), yOffset + (y * 10), 10, 10);
                        g.FillRectangle(new SolidBrush(GetGeneColor(individual.Genes[y * geneImageSideLength + x])), r);
                    }
                }
            }
        }

        private static void UpdatePng(string origin, string to, PopulationManager pop)
        {
            Bitmap b = (Bitmap)Bitmap.FromFile($"C:/Temp/{origin}");

            //if (pop.Inversed != null)
            //{
            //    pop.Inversed.ForEach(i => DrawInversionsOnGene(b, i));
            //    DrawInversionStartOnGene(b, pop.Inversed.First());
            //    DrawInversionEndOnGene(b, pop.Inversed.Last());
            //}
            //if (pop.Replaced.HasValue)
            //    DrawReplacedMarkOnGene(b, pop.Replaced.Value);
            //if (pop.Adapted != null)
            //    pop.Adapted.ForEach(i => DrawArrowOnGene(b, i));
            //if (pop.Flipped != null)
            //    DrawFlipMarkOnGene(b, pop.Flipped.Value);
            //if (pop.Swapped != null)
            //    DrawSwapMarkOnGenes(b, pop.Swapped);
            //if (pop.SplitSizes != null)
            //{
            //    int genes = 0;
            //    foreach (int split in pop.SplitSizes)
            //    {
            //        DrawSplitMarkStartOnGene(b, genes);
            //        genes += split;
            //        DrawSplitMarkEndOnGene(b, genes - 1);
            //    }
            //}

            b.Save($"C:/Temp/{to}", ImageFormat.Png);
        }

        private static void DrawSplitMarkStartOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 0, offsetY + 3, Color.SandyBrown);
            b.SetPixel(offsetX + 0, offsetY + 4, Color.SandyBrown);
            b.SetPixel(offsetX + 0, offsetY + 5, Color.SandyBrown);
            b.SetPixel(offsetX + 1, offsetY + 4, Color.SandyBrown);
        }

        private static void DrawSplitMarkEndOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 9, offsetY + 3, Color.SandyBrown);
            b.SetPixel(offsetX + 9, offsetY + 4, Color.SandyBrown);
            b.SetPixel(offsetX + 9, offsetY + 5, Color.SandyBrown);
            b.SetPixel(offsetX + 8, offsetY + 4, Color.SandyBrown);
        }

        private static void DrawSwapMarkOnGenes(Bitmap b, Tuple<int, int> swapped)
        {
            DrawSwapMarkOnGene(b, swapped.Item1);
            DrawSwapMarkOnGene(b, swapped.Item2);
        }

        private static void DrawSwapMarkOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 8, offsetY, Color.DarkBlue);
            b.SetPixel(offsetX + 9, offsetY, Color.DarkBlue);
            b.SetPixel(offsetX + 9, offsetY + 1, Color.DarkBlue);
        }

        private static void DrawReplacedMarkOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 6, offsetY + 6, Color.Yellow);
            b.SetPixel(offsetX + 6, offsetY + 7, Color.Yellow);
            b.SetPixel(offsetX + 6, offsetY + 8, Color.Yellow);
            b.SetPixel(offsetX + 6, offsetY + 9, Color.Yellow);

            b.SetPixel(offsetX + 7, offsetY + 7, Color.Yellow);
            b.SetPixel(offsetX + 8, offsetY + 8, Color.Yellow);

            b.SetPixel(offsetX + 9, offsetY + 6, Color.Yellow);
            b.SetPixel(offsetX + 9, offsetY + 7, Color.Yellow);
            b.SetPixel(offsetX + 9, offsetY + 8, Color.Yellow);
            b.SetPixel(offsetX + 9, offsetY + 9, Color.Yellow);
        }

        private static void DrawInversionEndOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 7, offsetY + 0, Color.LightBlue);
            b.SetPixel(offsetX + 8, offsetY + 1, Color.LightBlue);
            b.SetPixel(offsetX + 7, offsetY + 4, Color.LightBlue);
            b.SetPixel(offsetX + 8, offsetY + 3, Color.LightBlue);
        }

        private static void DrawInversionStartOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 1, offsetY + 1, Color.LightBlue);
            b.SetPixel(offsetX + 2, offsetY + 0, Color.LightBlue);
            b.SetPixel(offsetX + 1, offsetY + 3, Color.LightBlue);
            b.SetPixel(offsetX + 2, offsetY + 4, Color.LightBlue);
        }

        private static void DrawInversionsOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            for (int i = 0; i < 10; i++)
            {
                b.SetPixel(offsetX + i, offsetY + 2, Color.LightBlue);
            }
        }

        private static void DrawFlipMarkOnGene(Bitmap b, int gene)
        {
            int offsetX = (gene % 13) * 10;
            int offsetY = (gene / 13) * 10;

            b.SetPixel(offsetX + 1, offsetY + 8, Color.Orange);
            b.SetPixel(offsetX + 2, offsetY + 8, Color.Orange);
            b.SetPixel(offsetX + 3, offsetY + 8, Color.Orange);
        }

        private static void DrawArrowOnGene(Bitmap b, Tuple<int, Directions> i)
        {
            int offsetX = (i.Item1 % 13) * 10;
            int offsetY = (i.Item1 / 13) * 10;

            b.SetPixel(offsetX + 6, offsetY + 2, Color.Red);
            b.SetPixel(offsetX + 6, offsetY + 3, Color.Red);
            b.SetPixel(offsetX + 6, offsetY + 4, Color.Red);
            b.SetPixel(offsetX + 6, offsetY + 5, Color.Red);
            b.SetPixel(offsetX + 6, offsetY + 6, Color.Red);
            b.SetPixel(offsetX + 6, offsetY + 7, Color.Red);
            b.SetPixel(offsetX + 6, offsetY + 8, Color.Red);

            if (i.Item2 == Directions.Up)
            {
                b.SetPixel(offsetX + 5, offsetY + 3, Color.Red);
                b.SetPixel(offsetX + 7, offsetY + 3, Color.Red);
                b.SetPixel(offsetX + 4, offsetY + 4, Color.Red);
                b.SetPixel(offsetX + 8, offsetY + 4, Color.Red);
            }
            else
            {
                b.SetPixel(offsetX + 5, offsetY + 7, Color.Red);
                b.SetPixel(offsetX + 7, offsetY + 7, Color.Red);
                b.SetPixel(offsetX + 4, offsetY + 6, Color.Red);
                b.SetPixel(offsetX + 8, offsetY + 6, Color.Red);
            }
        }

        private static void SaveToPng(DNA individual, string name)
        {
            using (Bitmap b = new Bitmap(130, 130))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.Black);
                    for (int y = 0; y < 13; y++)
                    {
                        for (int x = 0; x < 13; x++)
                        {
                            if (y * 13 + x < individual.Genes.Count)
                            {
                                Rectangle r = new Rectangle(x * 10, y * 10, 10, 10);
                                g.FillRectangle(new SolidBrush(GetGeneColor(individual.Genes[y * 13 + x])), r);
                            }
                        }
                    }
                }
                b.Save($"C:/Temp/{name}", ImageFormat.Png);
            }
        }

        private static Color GetGeneColor(double v)
        {
            int white = 127;
            white = 128 + (int)Math.Round(Math.Clamp(white * v, -127, 127));
            return Color.FromArgb(white, white, white);
        }

    }
}
