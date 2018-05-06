# Microsoft Graph 控制臺應用程序模板

[English](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-console/README.md) | [简体中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-console/lang/zh-cn/README.md) | 繁體中文

> 作者：陳希章 發表於 2018年4月22日

## 概述

這個項目模板可以幫助妳快速建立壹個控制臺應用程序，並且以某個用戶的身份調用Microsoft Graph的服務，實現了如下三個功能

1. 讀取當前用戶的基本信息
1. 讀取當前用戶的郵箱收件箱（Exchange Online）中的前十封郵件
1. 讀取當前用戶的個人網盤（OneDrive for Business）的根目錄下面的文件列表

由於.NET Core是要跨平臺的，所以在用戶登陸這個行為上，不能采用原生的Windows操作系統中提供的彈出窗口，該模板采用了壹種特殊的方式來實現。程序運行後會自動生成壹個設備編號，並且給用戶提示壹個登陸的網頁地址，用戶在網頁上面輸入設備編號，會被引導到相應的Office 365的登陸頁面，完成身份驗證和授權後，控制臺程序將收到通知，並且獲取訪問票據（Access Token），然後就可以盡情地調用Microsoft Graph了。

該模板支持國際版，也支持國內版。

## 準備

為了使用該模板，妳最好能自行註冊壹個應用程序。如果妳對這方面的概念不熟悉，請參考 [Microsoft Graph overview](https://github.com/chenxizhang/office365dev/blob/master/docs/microsoftgraphoverview.md)的說明。

如果妳是需要訪問國際版Office 365，建議妳直接使用AAD 2.0的註冊方式，在<https://apps.dev.microsoft.com>進行註冊，請參考[這篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration2.0.md)。

如果妳是需要訪問國內版Office 365，妳目前只能使用AAD 1.0的註冊方式，在<https://portal.azure.cn>進行註冊，請參考[這篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration.md)。

這個模板範例，至少需要三個委派權限（Delegate Permission）

1. Files.Read.All
1. Mail.Read
1. User.Read

## 安裝

妳通過 `dotnet new -i chenxizhang.dotnetcore.msgraph.console.CSharp` 即可安裝這個項目模板。

## 使用

這個模板有幾個用法，分別如下

1. 最簡單的用法 `dotnet new graphconsole` 將創建壹個模板實現，妳將使用我預先創建好的壹個clientid訪問到Office 365國際版。
1. 通過指定`clientid`參數，明確使用妳的應用程序來訪問Office 365， 這是我最推薦的，語法是 `dotnet new graphconsole --clientid 妳創建的應用程序編號`。
1. 通過`instance`參數，指定妳要訪問的是國際版還是國內版。國際版是默認的，而如果要指定國內版，則需要用如下的語法 `dotnet new graphconsole --instance gallatin --clientid 妳創建的應用程序編號`。
1. 通過`version`參數，指定妳要訪問的Graph API的版本，默認為`v1.0`，目前還支持`beta`。

另外還有兩個通用參數

1. 通過指定`name` 可以改變模板生成的項目名稱，以及默認的namespace名稱。例如 `dotnet new graphconsole -n mynamespace`。
1. 通過指定`output`可以指定生成新的項目目錄。例如 `dotnet new graphconsole -o test`。

壹旦創建好項目，妳可以直接通過`dotnet run`運行，或者在`Visual Studio Code`中編輯後再運行。

## 卸載

妳可以通過 `dotnet new -u chenxizhang.dotnetcore.msgraph.console.CSharp`可以卸載當前這個項目模板。