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

        public static String converterASCII(Bitmap imagem)
        {
            String caracteres = "@%#*+=-:. ";
            imagem = converterEscalaCinza(imagem);
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
