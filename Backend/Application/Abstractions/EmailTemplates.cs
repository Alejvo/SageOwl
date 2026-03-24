namespace Application.Abstractions;

public class EmailTemplates
{
    public const string WelcomeEmail = """
        <!DOCTYPE html>
        <html lang="en">
          <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <link rel="stylesheet" href="src/style.css">
          </head>
         <body style="margin:0;padding:0;background:#DEDEDE;font-family:Arial,Helvetica,sans-serif;">
          <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="padding:20px 0;">
            <tr>
              <td align="center">
        
                <table role="presentation" width="480" cellpadding="0" cellspacing="0" style="background:#ffffff;border-radius:6px;padding:32px;box-sizing:border-box;">
        
                  <tr>
                    <td style="text-align:center;padding-bottom:10px;">
                      <h2 style="margin:0;font-size:20px;font-weight:600;color:#333;">Welcome to SageOwl</h2>
                    </td>
                  </tr>
        
                  <tr>
                    <td style="padding:16px 0;color:#555;font-size:15px;line-height:1.6;">
                      Hello <strong>{notification.Name}</strong>,<br><br>
                      Thank you for registering in <strong> SageOwl</strong>. Your account is ready to use.  
                    </td>
                  </tr>
        
                  <tr>
                    <td align="center" style="padding:20px 0;">
                      <a href="https://localhost:7157/" 
                         style="display:inline-block;background:#E8783F;color:#fff;text-decoration:none;padding:10px 20px;border-radius:4px;font-size:14px;">
                        Access my account
                      </a>
                    </td>
                  </tr>
        
                  <tr>
                    <td style="padding-top:10px;color:#777;font-size:13px;line-height:1.5;">
                      If you didn't create this account, you may disregard this message.<br>
                      If you require assistance, please contact us <span style="color:#000;">sample@sageowl.com</span>.
                    </td>
                  </tr>
        
                </table>
        
                <p style="margin-top:20px;font-size:12px;color:#999;">
                  © 2025 SageOwl
                </p>
        
              </td>
            </tr>
          </table>
        </body>
        </html>
        """;
}
