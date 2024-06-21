
### Project Description
This sample uses System.Net.Mail’s SmtpClient to send emails.   It was created to demonstrate to developers how to use this API and includes most of the most used features of this API.
This is an open source application and no warranties or guarantees are made. 

### Features

System.Net.Mail features included:	
•	Send email
•	Add custom header(s)
•	Add attachment(s)
•	Add Alternate views (html and plain text)
•	Add LinkedResource / Inline attachment
•	Set message priority
•	Use html body for message
•	Use read receipt
•	Send by port
•	Send by pickup folder
•	Send message every ‘x’ number of seconds
•	Send to multiple recipients (To, Cc, Bcc)
•	Adjust file attachment content type
•	Adjust message encoding
•	Send calendar appointments
•	Error logging

### Requirements

Microsoft .NET Framework Version 4
 
### Release Notes

Tool link:  dseph/NetMail-Sender-Tool-using-System.Net.Mail (github.com)
 
### Footnotes

Special thanks to the following articles for providing me with helpful information.

I looked at the code from the following sites to base some of the functionality of the NetMail features:

  http://systemnetmail.com
  http://blogs.msdn.com/b/webdav_101/
  
The function to convert the file size and display it was primarily taken from the following post on stackoverflow:

     http://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
     
Image Resources are from:

     http://www.microsoft.com/en-us/download/details.aspx?id=35825
     
Logging sample borrowed and modified from EWS Streaming Notification
    http://ewsstreaming.codeplex.com/
