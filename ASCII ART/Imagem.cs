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
        private static Bitmap converterEscalaCinza(Bitmap imagemOriginal)
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
            for(int i = 0, m = 0; i + escala < imagemOriginal.Height; i += escala, m++)
            {
                for(int j = 0, n = 0; j + escala < imagemOriginal.Width; j += escala, n++)
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

        public static String converterASCII(Bitmap imagem)
        {
            String caracteres = "@%#*+=-:. ";
            imagem = converterEscalaCinza(imagem);
            //imagem = converterEscalaCinza(reduzirEscala(imagem));  
            String texto = null;
            for (int i = 0; i < imagem.Height; i++)
            {
                for (int j = 0; j < imagem.Width; j++)
                {
                    int cor = imagem.GetPixel(j, i).R;
                    texto += Convert.ToChar(caracteres[cor*caracteres.Length/255]);
                }
                texto += System.Environment.NewLine;
            }
            return texto;
        }

    }
}
