# Microsoft Graph 服務應用程序模板

[English](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-serviceapp/README.md) | [简体中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-serviceapp/lang/zh-cn/README.md) | 繁體中文

> 作者：陳希章 發表於 2018年4月22日

## 概述

服務應用程序指的是不需要用戶交互的壹類應用程序，他們很可能是通過定時器在後臺運行。由於這種特性，它們在訪問Microsoft Graph時往往不是以某個特定的用戶身份，而是用壹個固定的身份來完成。在這個項目模板中，我們實現的是Oauth 2.0中標準的Client Credential 這種認證流程，即應用程序使用自己的應用程序編號（`clientid`），和壹個密鑰（`secret`）來表明自己的身份，而它獲得的權限，在得到Office 365 管理員確認後，可以訪問任何用戶的數據。

該模板實現了五個基本功能

1. 讀取所有用戶信息
1. 讀取某個用戶的收件箱郵件列表
1. 讀取某個用戶的個人網盤根目錄的文件列表
1. 代表某個用戶發送郵件
1. 給某個用戶的個人網盤上傳壹個文本文件

## 準備

為了使用該模板，妳最好能自行註冊壹個應用程序。如果妳對這方面的概念不熟悉，請參考 [Microsoft Graph overview](https://github.com/chenxizhang/office365dev/blob/master/docs/microsoftgraphoverview.md)的說明。

如果妳是需要訪問國際版Office 365，建議妳直接使用AAD 2.0的註冊方式，在<https://apps.dev.microsoft.com>進行註冊，請參考[這篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration2.0.md)。

如果妳是需要訪問國內版Office 365，妳目前只能使用AAD 1.0的註冊方式，在<https://portal.azure.cn>進行註冊，請參考[這篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration.md)。

請在註冊應用程序後，生成壹個新的密鑰（secret），並且復制保存起來。

這個模板範例，至少需要三個應用權限（Application Permission）

1. Files.Read.All（Admin）
1. Mail.Read（Admin）
1. User.Read.All(Admin)
1. Mail.Send(Admin)
1. File.ReadWrite.All(Admin)

請註意，這壹類應用權限，需要得到Office 365管理的確認（Admin consent），項目生成後，還會有這方面的操作提示。

## 安裝

妳通過 `dotnet new -i chenxizhang.dotnetcore.msgraph.serviceapp.CSharp` 即可安裝這個項目模板。

## 使用

這個項目模板定義了五個參數，分別如下

1. `instance`，用來定義目標Office 365環境的版本，`global`代表國際版（默認），`gallatin`代表國內版。
1. `clientid`,註冊應用程序時獲得的應用程序編號。
1. `secret`,註冊應用程序時生成的密鑰。
1. `tenantid`，指定妳要最終訪問的租戶id。不同於委派權限，應用程序權限都必須要明確地限定到具體的租戶，而不是多租戶的。這個tenantid有幾種方式獲取，壹種是妳在做Admin consent的時候，在返回結果的地址欄中有這個信息。妳也可以通過[我的這篇文章](http://www.cnblogs.com/chenxizhang/p/7904293.html)所介紹的PowerShell方法獲取。
1. `version`，指定妳要訪問的Graph API的版本，默認為`v1.0`，目前還支持`beta`。

如果妳還不太清楚怎麽註冊應用程序，以及如何做Admin consent，妳可以直接運行最基本的壹個命令`dotnet new graphserviceapp`，使用我配置好的壹個環境體驗壹下這幾個功能。
> 我的這個測試環境經常會修改，而且可能會過期，所以可能妳在用的時候會遇到無法正常工作的情況。

該模板的常規使用方法是至少指定前四個參數，如 `dotnet new graphserviceapp --instance gallatin --clientid 妳的應用程序編號 --secret 妳的密鑰 --tenantid 妳的租戶編號`。

另外還有兩個通用參數

1. 通過指定`name` 可以改變模板生成的項目名稱，以及默認的namespace名稱。例如 `dotnet new graphserviceapp -n myserviceapp`。
1. 通過指定`output`可以指定生成新的項目目錄。例如 `dotnet new graphserviceapp -o testapp`。

另外，請註意，項目生成後妳不能直接就`dotnet run`進行運行，而是至少修改兩行代碼（第72，73行），設置妳要訪問的用戶upn和郵件收件人信息。

## 卸載

妳可以通過 `dotnet new -u chenxizhang.dotnetcore.msgraph.serviceapp.CSharp`可以卸載當前這個項目模板。