// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.Utility
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace m2ostnextservice.Models
{
  public class Utility
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public void eventLog(string str)
    {
      bool flag = Directory.Exists(HttpContext.Current.Server.MapPath("~/Content/Log/"));
      string str1 = DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
      DateTime now = DateTime.Now;
      if (!flag)
        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Log/"));
      string path = HttpContext.Current.Server.MapPath("~/Content/Log/") + str1;
      if (!System.IO.File.Exists(path))
        System.IO.File.Create(path);
      using (StreamWriter streamWriter = System.IO.File.AppendText(path))
      {
        string[] strArray = new string[1]
        {
          "timestamp : " + now.ToString("dd-MM-yyyy HH:mm:ss") + " : " + str
        };
        foreach (string str2 in strArray)
          streamWriter.WriteLine(str2);
      }
    }

    public bool checkCategoryContentCount(int cid, int oid, int uid)
    {
      bool flag = false;
      try
      {
        if (new CategoryModel().getContentInCategoryCount("SELECT count(*) COUNT FROM tbl_content WHERE STATUS='A'  AND id_content IN (select id_content from tbl_content_user_assisgnment where id_category=" + cid.ToString() + " AND id_user=" + uid.ToString() + " AND id_organization=" + oid.ToString() + " )") > 0)
          return true;
        if (new CategoryModel().getContentInCategoryCount("SELECT count(*) COUNT  FROM tbl_content WHERE STATUS='A'  AND id_content IN (select id_content from tbl_content_organization_mapping where id_category=" + cid.ToString() + " AND id_organization=" + oid.ToString() + " and STATUS='A'  ) ORDER BY CONTENT_QUESTION ") > 0)
          return true;
        if (new CategoryModel().getContentInCategoryCount("select count(*) COUNT  from tbl_assessment_sheet where id_assesment in (select id_assessment from tbl_assessment_user_assignment where id_category=" + cid.ToString() + " AND id_user=" + uid.ToString() + ") OR id_assesment in (select id_assessment from tbl_assessment_categoty_mapping where id_category=" + cid.ToString() + ") and status='A'") > 0)
          return true;
      }
      catch (Exception ex)
      {
        new Utility().eventLog("ex m :" + ex.Message);
        new Utility().eventLog("ex s :" + ex.StackTrace);
        if (ex.InnerException != null)
          new Utility().eventLog("ex i :" + ex.InnerException.ToString());
      }
      return flag;
    }

    public string SendMail(string TO, string FROM, string body, string SUBJECT)
    {
      string str = "Mail sent to : ";
      try
      {
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          UseDefaultCredentials = false,
          Credentials = ((ICredentialsByHost) new NetworkCredential("info@paathshala.biz", "3203kalokhesadan")),
          EnableSsl = true
        }.Send(new MailMessage()
        {
          To = {
            TO
          },
          From = new MailAddress(FROM),
          Subject = SUBJECT,
          BodyEncoding = Encoding.UTF8,
          Body = body,
          IsBodyHtml = true
        });
      }
      catch (SmtpFailedRecipientException ex)
      {
        switch (ex.StatusCode)
        {
          case SmtpStatusCode.MailboxBusy:
          case SmtpStatusCode.TransactionFailed:
            str = " Mail box is busy|Mailbox is unavailable|Transaction is failed ";
            break;
          case SmtpStatusCode.MailboxUnavailable:
            str = "Email address is unavailable ";
            break;
          default:
            str = "Exception : " + ex.Message;
            if (ex.InnerException != null)
            {
              str = str + " [" + ex.InnerException.Message + "]";
              break;
            }
            break;
        }
      }
      catch (Exception ex)
      {
        str = "Exception : " + ex.Message;
        if (ex.InnerException != null)
          str = str + " [" + ex.InnerException.Message + "]";
      }
      return str;
    }

    public bool IsValidEmail(string email)
    {
      try
      {
        return new MailAddress(email).Address == email;
      }
      catch
      {
        return false;
      }
    }

    public string SendMailError(string TO, string FROM, string body, string SUBJECT)
    {
      string str = "Mail sent to : ";
      try
      {
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          UseDefaultCredentials = false,
          Credentials = ((ICredentialsByHost) new NetworkCredential()),
          EnableSsl = true
        }.Send(new MailMessage()
        {
          To = {
            TO
          },
          From = new MailAddress(FROM),
          Subject = SUBJECT,
          BodyEncoding = Encoding.UTF8,
          Body = body,
          IsBodyHtml = true
        });
      }
      catch (SmtpFailedRecipientException ex)
      {
        switch (ex.StatusCode)
        {
          case SmtpStatusCode.MailboxBusy:
          case SmtpStatusCode.TransactionFailed:
            str = " Mail box is busy|Mailbox is unavailable|Transaction is failed ";
            break;
          case SmtpStatusCode.MailboxUnavailable:
            str = "Email address is unavailable ";
            break;
          default:
            str = "Exception : " + ex.Message;
            if (ex.InnerException != null)
            {
              str = str + " [" + ex.InnerException.Message + "]";
              break;
            }
            break;
        }
      }
      catch (Exception ex)
      {
        str = "Exception : " + ex.Message;
        if (ex.InnerException != null)
          str = str + " [" + ex.InnerException.Message + "]";
      }
      return str;
    }

    public string mysqlTrim(string str)
    {
      char[] chArray = new char[3]{ '\'', '%', '=' };
      string[] strArray = new string[4]
      {
        "LIKE",
        "SELECT",
        "INSERT",
        "--"
      };
      str = str.Trim(chArray);
      str = str.Replace("LIKE", "");
      str = str.Replace("--", "");
      return Regex.Replace(str, "[\\x00'\"\\b\\n\\r\\t\\cZ\\\\%]", (MatchEvaluator) (match =>
      {
        string str1 = match.Value;
        switch (str1)
        {
          case "\0":
            return "\\0";
          case "\b":
            return "\\b";
          case "\n":
            return "\\n";
          case "\r":
            return "\\r";
          case "\t":
            return "\\t";
          case "\u001A":
            return "\\Z";
          default:
            return "\\" + str1;
        }
      }));
    }

    public string uniqueIDS(int length)
    {
      Random random = new Random();
      return new string(Enumerable.Repeat<string>("ABCDEFGHIJKL01234MNOPQRSTUVWXYZ56789", length).Select<string, char>((Func<string, char>) (s => s[random.Next(s.Length)])).ToArray<char>());
    }
  }
}
