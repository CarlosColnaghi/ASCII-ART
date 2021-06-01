using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_ART
{
    public static class Imagem
    {
        private static Bitmap converterCinzaClassico(Bitmap imagemOriginal)
        {
            Bitmap imagemProcessada = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            for (int i = 0; i < imagemOriginal.Height; i++)
            {
                for (int j = 0; j < imagemOriginal.Width; j++)
                {
                    Color pixel = imagemOriginal.GetPixel(j, i);
                    int cor = (int)(0.29 * pixel.R + 0.59 * pixel.G + 0.11 * pixel.B);
                    imagemProcessada.SetPixel(j, i, Color.FromArgb(255, cor, cor, cor));
                }
            }
            return imagemProcessada;
        }

        private static Bitmap reduzirEscala(Bitmap imagemOriginal, int escala = 1)
        {
            Bitmap imagemProcessada = new Bitmap(imagemOriginal.Width/escala, imagemOriginal.Height/escala);
            for(int i = 0, m = 0; (i + escala) <= imagemOriginal.Height; i += escala, m++)
            {
                for(int j = 0, n = 0; (j + escala) <= imagemOriginal.Width; j += escala, n++)
                {
                    int[] soma = { 0, 0, 0 };
                    for(int k = i; k < i + escala; k++)
                    {
                        for(int l = j; l < j + escala; l++)
                        {
                            soma[0] += imagemOriginal.GetPixel(l, k).R;
                            soma[1] += imagemOriginal.GetPixel(l, k).G;
                            soma[2] += imagemOriginal.GetPixel(l, k).B;
                        }
                    }
                    int[] media = new int[3];
                    for(int o = 0; o < soma.Length; o++)
                    {
                        media[o] = soma[o] / (escala * escala);
                    }
                    imagemProcessada.SetPixel(n, m, Color.FromArgb(255, media[0], media[1], media[2]));
                }
            }
            return imagemProcessada;
        }

        public static String converterASCII(Bitmap imagem, int escala = 1)
        {
            String texto = null;
            if (!imagem.Equals(null))
            {
                String caracteres = "@%#*+=-:.  ";
                imagem = converterCinzaClassico(reduzirEscala(imagem, escala));

                for (int i = 0; i < imagem.Height; i++)
                {
                    for (int j = 0; j < imagem.Width; j++)
                    {
                        int cor = imagem.GetPixel(j, i).R;
                        texto += Convert.ToChar(caracteres[cor * (caracteres.Length - 1) / 255]);
                    }
                    texto += System.Environment.NewLine;
                }
            }
            return texto;
        }

        private static Bitmap converterCinzaMaximoCanal(Bitmap imagemOriginal)
        {
            Bitmap imagemProcessada = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            for (int i = 0; i < imagemOriginal.Height; i++)
            {
                for (int j = 0; j < imagemOriginal.Width; j++)
                {
                    Color pixel = imagemOriginal.GetPixel(j, i);
                    int[] cores = { pixel.R, pixel.G, pixel.B };
                    imagemProcessada.SetPixel(j, i, Color.FromArgb(255, cores.Max(), cores.Max(), cores.Max()));
                }
            }
            return imagemProcessada;
        }

        private static Bitmap convoluirMascara(Bitmap imagemOriginal, int[,] mascara)
        {
            Bitmap imagemProcessada = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            for (int i = 1; i <= imagemOriginal.Height - 2; i++)
            {
                for (int j = 1; j <= imagemOriginal.Width - 2; j++)
                {
                    int soma = 0;
                    for (int k = i - 1, m = 0; k <= i + 1; k++, m++)
                    {
                        for (int l = j - 1, n = 0; l <= j + 1; l++, n++)
                        {
                            soma += imagemOriginal.GetPixel(l, k).R * mascara[m, n];
                        }
                    }
                    if (soma < 0)
                    {
                        soma = 0;
                    }
                    else if (soma > 255)
                    {
                        soma = 255;
                    }
                    imagemProcessada.SetPixel(j, i, Color.FromArgb(255, soma, soma, soma));
                }
            }
            return imagemProcessada;
        }

        private static Bitmap realcarBorda(Bitmap imagem, int mascara = 1)
        {
            return convoluirMascara(imagem, Mascara.getLaplace(mascara));
        }

        public static String converterASCIIBorda(Bitmap imagem, int escala = 1)
        {
            String texto = null;
            if (!imagem.Equals(null))
            {
                String caracteres = "  .:-= +*#%@";
                imagem = realcarBorda(converterCinzaMaximoCanal(reduzirEscala(imagem, escala)));

                for (int i = 0; i < imagem.Height; i++)
                {
                    for (int j = 0; j < imagem.Width; j++)
                    {
                        int cor = imagem.GetPixel(j, i).R;
                        texto += Convert.ToChar(caracteres[cor * (caracteres.Length - 1) / 255]);
                    }
                    texto += System.Environment.NewLine;
                } 
            }
            return texto;
        }
    }
}
