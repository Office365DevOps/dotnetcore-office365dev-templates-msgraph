# Microsoft Graph Web API 模板

[English](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-webapi/README.md) | [简体中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-webapi/lang/zh-cn/README.md) | [繁體中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-webapi/lang/zh-tw/README.md)

> 作者：陳希章 發表於 2018年5月1日

## 概述

這個項目模板可以幫助妳快速建立壹個服務項目，並且可以接受客戶端調用，通過On-Behalf-Of認證流實現以某個用戶的身份調用Microsoft Graph的服務，實現了如下壹個功能

1. 讀取當前用戶的基本信息

該模板支持國際版，也支持國內版。

## 準備

為了使用該模板，妳最好能自行註冊壹個應用程序。如果妳對這方面的概念不熟悉，請參考 [Microsoft Graph overview](https://github.com/chenxizhang/office365dev/blob/master/docs/microsoftgraphoverview.md)的說明。

不管妳是使用國際版還是國內版，妳目前只能使用AAD 1.0的註冊方式，在<https://portal.azure.cn>進行註冊，請參考[這篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration.md)。

這個模板範例，至少需要壹個委派權限（Delegate Permission）

1. User.Read

這個模板實現了壹套完整的On-Behalf-Of認證流，即客戶端應用程序先自行從Azure AD中得到訪問許可，然後再調用中間層服務的時候，服務層會使用客戶端的Access_Token去換取自己的Access_Token，看起來就好像客戶端自己去訪問壹樣。關於這個場景，以及如何配置On-Behalf-Of的應用程序和權限，請參考 <https://github.com/chenxizhang/active-directory-dotnet-webapi-onbehalfof> 這裏的說明。妳需要註冊兩個應用程序。


## 安裝

妳通過 `dotnet new -i chenxizhang.dotnetcore.msgraph.webapi.CSharp` 即可安裝這個項目模板。

## 使用

這個模板有幾個用法，分別如下

1. 最簡單的用法 `dotnet new graphwebapi` 將創建壹個模板實現，妳將使用我預先創建好的壹個clientid訪問到Office 365國際版。
1. 通過指定`clientid`參數和`secret`參數、`tenantid`參數、`obo-console-clientid`參數，明確使用妳的應用程序來訪問Office 365， 這是我最推薦的，語法是 `dotnet new graphwebapi --clientid 妳創建的應用程序編號 --secret 妳的密鑰 --tenantid 妳的租戶編號 --obo-console-clientid 妳為客戶端註冊的應用程序編號`。關於如何獲取`tenantid`,妳也可以通過[我的這篇文章](http://www.cnblogs.com/chenxizhang/p/7904293.html)所介紹的PowerShell方法獲取。
1. 通過`instance`參數，指定妳要訪問的是國際版還是國內版。國際版是默認的，而如果要指定國內版，則需要用如下的語法 `dotnet new graphwebapi --instance gallatin --clientid 妳創建的應用程序編號 --secret 妳的密鑰`。
1. 通過`version`參數，指定妳要訪問的Graph API的版本，默認為`v1.0`，目前還支持`beta`。

另外還有兩個通用參數

1. 通過指定`name` 可以改變模板生成的項目名稱，以及默認的namespace名稱。例如 `dotnet new graphwebapi -n mynamespace`。
1. 通過指定`output`可以指定生成新的項目目錄。例如 `dotnet new graphwebapi -o test`。

壹旦創建好項目，妳可以直接通過`dotnet run`運行，或者在`Visual Studio Code`中編輯後再運行。

1. 讀取當前用戶的基本信息

## 卸載

妳可以通過 `dotnet new -u chenxizhang.dotnetcore.msgraph.webapi.CSharp`可以卸載當前這個項目模板。