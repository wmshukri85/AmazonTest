# AmazonTest
Selenium test on Amazon search and assert price

Test code in: Amazon01/TestCases

Nuget:
Microsoft.Net.Test.sdk
Nunit
NUnit3TestAdapter
Selenium.Support
Selenium.Webdriver

Issue:
- On some occasion the Amazon page will block the page with Captcha screen. Added checking in the code if this page appears and add 10 sec waits but require manual intervention to enter the code.
