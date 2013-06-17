namespace wScreenshot.UploadModule
{
    public class EpvpImg
    {
        //public string Upload3(Bitmap bmp, ImageFormat imgf, string fileName, string user, string pass)
        //{
        //    MemoryStream bmps = new MemoryStream();
        //    string ret;
        //    try
        //    {
        //        save(bmps, bmp, imgf);

        //        //bmp.Save(bmps, imgf);
        //    }
        //    catch (Exception)
        //    {
        //        Bitmap nb = new Bitmap(bmp.Width, bmp.Height);
        //        BitmapData nbd = nb.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        //        BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        //        unsafe
        //        {
        //            int* startBn = (int*)nbd.Scan0;
        //            int* startB = (int*)bd.Scan0;
        //            for (int y = 0; y < bd.Height; y++)
        //            {
        //                for (int x = 0; x < bd.Width; x++)
        //                {
        //                    startBn[x + y * bd.Width] = startB[x + y * bd.Width];
        //                }
        //            }
        //            bmp.UnlockBits(bd);
        //            nb.UnlockBits(nbd);
        //        }
        //        bmps = new MemoryStream();
        //        save(bmps, bmp, imgf);
        //        nb.Dispose();
        //    }
        //    try
        //    {
        //        byte[] fileBytes = bmps.ToArray();

        //        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://epvpimg.com/upload/");
        //        System.Net.ServicePointManager.Expect100Continue = false;

        //        //request.ServicePoint.Expect100Continue = false;
        //        //glob
        //        request.Proxy = FormOptions.proxy;

        //        //request.PreAuthenticate = true;
        //        //request.AllowWriteStreamBuffering = true;
        //        string boundary = System.Guid.NewGuid().ToString();

        //        request.Referer = "http://nightow.de/";
        //        request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
        //        request.Method = "POST";

        //        request.UserAgent = "wScreenshot";

        //        // Build Contents for Post
        //        string header = string.Format("--{0}", boundary);
        //        string footer = header + "--";

        //        StringBuilder contents = new StringBuilder();
        //        StringBuilder contents2 = new StringBuilder();

        //        // File
        //        contents.AppendLine(header);

        //        contents.AppendLine(string.Format("Content-Disposition: form-data; name=\"files[]\"; filename=\"{0}\"", fileName));
        //        contents.AppendLine("Content-Type:image/" + imgf.ToString());
        //        contents.AppendLine();

        //        // file goes here... then contents 2

        //        //-----------------------------262032899517762
        //        //Content-Disposition: form-data; name="options[resize]"

        //        //-0
        //        //-----------------------------262032899517762
        //        //Content-Disposition: form-data; name="options[resize_width]"

        //        //-----------------------------262032899517762
        //        //Content-Disposition: form-data; name="options[resize_height]"

        //        contents2.AppendLine();

        //        // Form Field 1
        //        contents2.AppendLine(header);
        //        contents2.AppendLine("Content-Disposition:form-data; name=\"options[resize]\"");
        //        contents2.AppendLine();
        //        contents2.AppendLine("-0");

        //        // Form Field 2
        //        contents2.AppendLine(header);
        //        contents2.AppendLine("Content-Disposition:form-data; name=\"options[resize_width]\"");
        //        contents2.AppendLine();

        //        // Form Field 3
        //        contents2.AppendLine(header);
        //        contents2.AppendLine("Content-Disposition:form-data; name=\"options[resize_height]\"");
        //        contents2.AppendLine();
        //        contents2.AppendLine();

        //        // Footer
        //        contents2.AppendLine(footer);

        //        byte[] bytes = Encoding.UTF8.GetBytes(contents.ToString());
        //        byte[] bytes2 = Encoding.UTF8.GetBytes(contents2.ToString());

        //        request.ContentLength = bytes.Length + fileBytes.Length + bytes2.Length;
        //        ret = "error";
        //        byte[] b = new byte[bytes.Length + bytes2.Length + fileBytes.Length];
        //        bytes.CopyTo(b, 0);
        //        fileBytes.CopyTo(b, bytes.Length);
        //        bytes2.CopyTo(b, bytes.Length + fileBytes.Length);
        //        using (Stream s = request.GetRequestStream())
        //        {
        //            int proz = 0;
        //            int sendAtOneTime = 4096;
        //            int sent = 0;
        //            for (int i = 0; sent < b.Length; i++)
        //            {
        //                s.Write(b, sent, (b.Length - sent < sendAtOneTime) ? b.Length - sent : sendAtOneTime);
        //                sent += (b.Length - sent < sendAtOneTime) ? b.Length - sent : sendAtOneTime;
        //                proz = (int)(((double)sent / (double)b.Length) * 90.0);

        //                if (proz != bt_upload.Value) bt_upload.Value = proz;
        //                s.Flush();
        //            }

        //            //requestStream.Write(bytes, 0, bytes.Length);
        //            //requestStream.Write(fileBytes, 0, fileBytes.Length);
        //            //requestStream.Write(bytes2, 0, bytes2.Length);
        //            s.Flush();
        //            s.Close();

        //            using (WebResponse response = request.GetResponse())
        //            {
        //                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //                {
        //                    ret = reader.ReadToEnd();
        //                    Match m = Regex.Match(ret, "thumbnail_url\":\"(.*?)\"", RegexOptions.IgnoreCase);
        //                    if (m.Success)
        //                    {
        //                        ret = m.Groups[1].Value.Replace("\\", "");
        //                    }
        //                    else ret = "Error";
        //                }
        //            }
        //        }
        //        bt_upload.Value = 100;
        //        upLoadLink(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        ret = ex.ToString();
        //        bt_upload.Value = 0;
        //        uploadComplete();
        //    }

        //    return ret;
        //}
    }
}