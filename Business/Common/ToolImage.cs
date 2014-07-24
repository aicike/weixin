using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;

namespace Business.Common
{
    public class ToolImage
    {

        /// <summary>
        /// 裁剪，压缩方法 传Image对象
        /// </summary>
        /// <param name="imgSrc">原始图片</param>
        /// <param name="SavePath">保存路径(null表示存在临时目录，有值则保存在值所在目录)</param>
        /// <param name="flag">压缩比例，一般设置在70-100之间</param>
        /// <param name="thumbWidth">缩略图宽度 无 传0</param>
        /// <param name="thumbHeight">缩略图高度 无 传0</param>
        /// <param name="sm">抗锯齿</param>
        /// <param name="cq">影像合成</param>
        /// <param name="im">图片质量</param>
        /// <returns></returns>
        public static bool SuperGetPicThumbnail(HttpPostedFileBase oldFile, ref string SavePath, int flag, int thumbWidth, int thumbHeight, SmoothingMode sm = SmoothingMode.HighQuality, CompositingQuality cq = CompositingQuality.HighQuality, InterpolationMode im = InterpolationMode.High)
        {
            int dataLengthToRead = (int)oldFile.InputStream.Length;//获取下载的文件总大小
            byte[] buffer = new byte[dataLengthToRead];
            int r = oldFile.InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
            Stream tream = new MemoryStream(buffer);
            Image image = Image.FromStream(tream);
            Bitmap bmp = null;
            System.Drawing.Graphics gr = null;
            EncoderParameter eParam = null;
            EncoderParameters ep = null;
            try
            {
                //接着创建一个System.Drawing.Bitmap对象，并设置你希望的缩略图的宽度和高度。
                int srcWidth = image.Width;
                int srcHeight = image.Height;
                if (thumbWidth == 0 && thumbHeight == 0)
                {
                    thumbWidth = srcWidth;
                    thumbHeight = srcHeight;
                }
                else if (thumbWidth != 0 && thumbHeight != 0)
                {
                    if (srcWidth < thumbWidth)
                    {
                        thumbWidth = srcWidth;
                        thumbHeight = srcHeight;
                    }
                    else if (srcHeight < thumbHeight)
                    {
                        thumbWidth = srcWidth;
                        thumbHeight = srcHeight;
                    }
                }
                else
                {
                    if (thumbWidth != 0)
                    {
                        if (srcWidth < thumbWidth)
                        {
                            thumbWidth = srcWidth;
                            thumbHeight = srcHeight;
                        }
                        else
                        {
                            thumbHeight = Convert.ToInt32((srcHeight * 1.0 / srcWidth) * thumbWidth);
                        }
                    }
                    else
                    {
                        if (srcHeight < thumbHeight)
                        {
                            thumbWidth = srcWidth;
                            thumbHeight = srcHeight;
                        }
                        else
                        {
                            thumbWidth = Convert.ToInt32((srcWidth * 1.0 / srcHeight) * thumbHeight);
                        }

                    }
                }


                bmp = new Bitmap(thumbWidth, thumbHeight);
                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                gr = System.Drawing.Graphics.FromImage(bmp);
                //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
                gr.SmoothingMode = sm;
                //下面这个也设成高质量
                gr.CompositingQuality = cq;
                //下面这个设成High
                gr.InterpolationMode = im;


                //把原始图像绘制成上面所设置宽高的缩小图
                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
                ImageFormat tFormat = image.RawFormat;
                //以下代码为保存图片时，设置压缩质量 
                ep = new EncoderParameters();
                long[] qy = new long[1];
                qy[0] = flag;//设置压缩的比例1-100 
                eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
                ep.Param[0] = eParam;
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }

                #region 创建临时文件夹
                string Path = null;
                string sPath = null;
                if (SavePath == null)
                {
                    Path = "/File/Temp/" + DateTime.Now.ToString("yyyy-MM-dd"); string TempPath = HttpContext.Current.Server.MapPath(Path);
                    if (Directory.Exists(TempPath) == false)
                    {
                        Directory.CreateDirectory(TempPath);
                    }
                    string filename = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + oldFile.FileName;
                    SavePath = Path + filename;
                    sPath = TempPath + filename;
                }
                else {
                    sPath = SavePath;
                }
                #endregion

                if (jpegICIinfo != null)
                {
                    bmp.Save(sPath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    bmp.Save(sPath, tFormat);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
                if (ep != null)
                {
                    ep.Dispose();
                }
                if (eParam != null)
                {
                    eParam.Dispose();
                }
                if (gr != null)
                {
                    gr.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }


    }
}
