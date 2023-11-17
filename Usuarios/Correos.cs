using System.Net.Mail;
public class Correos{
    public string Destinatario {get; set;} = "";
    public string Asunto {get; set;} = "";
    public string Mensaje {get; set;} = "";

    public void Enviar(){
        MailMessage correo = new MailMessage();
        correo.From = new MailAddress ("dannell.angui@gmail.com", null, System.Text.Encoding.UTF8);
        correo. To.Add (Destinatario); 
        correo.Subject = this.Asunto;  
        correo.Body = this.Mensaje; 
        correo.IsBodyHtml = true;
        correo.Priority = MailPriority.Normal;
        SmtpClient smtp = new SmtpClient();
        smtp.UseDefaultCredentials = false;
        smtp.Host = "smtp.gmail.com"; 
        smtp.Port = 25; 
        smtp.Credentials = new System.Net.NetworkCredential ("dannell.angui@gmail.com", "tuwx zbss ldkg kjlu");
        smtp.EnableSsl = true;
        smtp.Send(correo);
    }
}