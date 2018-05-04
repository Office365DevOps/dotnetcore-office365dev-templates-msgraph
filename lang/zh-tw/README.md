# Office 365 應用開發的 .NET Core 模板庫

[English](../../readme.md) | [简体中文](../../lang/zh-cn/readme.md) | 繁体中文

> 作者：陳希章 發表於 2018年4月22日

## 概述

前不久我寫過壹篇文章[擁抱開源，Office 365開發迎來新時代](https://github.com/chenxizhang/office365dev/blob/master/docs/office365opensource.md)，給大家介紹了Office 365開發的典型場景是如何支持開源平臺的：Office 365通過Microsoft Graph，以REST API的方式提供服務，任何開發平臺都可以很方便地與其進行集成；Office Add-ins，SharePoint Add-ins和Microsoft Teams Apps開發，都可以基於標準的Web開發技術棧來實現，而且官方提供了對NodeJS，React，TypeScript等主流平臺和框架的默認支持（工具和模板層面都有）。

文章發表之後，我收到不少反饋，其實我內心深處牽掛的還有廣大的.NET開發人員啊。大家知道，.NET從頭到腳都是已經完全開源了的，針對Office 365的這些開發場景，是否有開箱即用的模板可供大家使用呢？利用周末的時間，我開始了這個新的項目，就是為大家整理出來壹套標準的.NET Core模板庫，歡迎用妳最熟悉的姿勢關註 <https://github.com/chenxizhang/dotnetcore-office365dev-templates>，妳可以簡單粗暴地直接給我 `star`,也可以 `fork` 這個項目通過 `pull request` 提交妳的模板，妳還可以通過`issue`通道給我反饋問題。

這壹套模板庫，不僅僅可以降低廣大的.NET 開發人員（準確地說是.NET Core開發人員）學習和使用Office 365開發平臺的門檻（尤其是如何在不同的場景下完成OAuth認證以及快速通過實例學習Graph的典型功能），還有壹個獨特的價值是可以讓妳在國際版和國內版這兩個平臺的選擇和切換過程中少走壹些彎路，我相信真正做過這方面開發的朋友們現在壹定是熱淚盈眶的表情。

希望大家喜歡！

## 計劃支持的模板

|模板|模板標識|模板短名稱|國際版|國內版|
|:---|:---|:---|:---|:---|
|控制臺應用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />console.<br />CSharp|[graphconsole](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-console/README.md)|支持|支持|
|無人值守應用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />serviceapp.<br />CSharp|[graphserviceapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-serviceapp/README.md)|支持|支持|
|ASP.NET Web應用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />webapp.<br />CSharp|[graphwebapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-webapp/README.md)|支持|支持|
|ASP.NET MVC應用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />mvcapp.<br />CSharp|[graphmvcapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-mvcapp/README.md)|支持|支持|
|ASP.NET Web API 應用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />mvcapi.<br />CSharp|[graphwebapi](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-webapi/README.md)|支持|支持|
|Office Add-ins （Excel）|即將提供|即將提供|支持|支持|
|SharePoint Add-ins |即將提供|即將提供|支持|支持|
|Microsoft Teams Apps |即將提供|即將提供|支持|暫不支持|

## 先決條件

要使用這些模板，妳只需要在開發機器上面安裝了 .NET Core SDK 版本2.0 及以上即可。請通過官方網站提示到的方式進行下載和安裝(<https://www.microsoft.com/net/download/>)。請註意，我們現在是跨平臺的哦，無論Windows，還是Mac，或是Linux都支持進行.NET Core開發，與此同時, .NET Core應用程序可以使用`docker`進行部署，其開發和部署、運營流程結合`devops`也是如絲般潤滑的。

另外，妳可能至少需要壹款代碼編輯器，妳可以安裝Visual Studio，也可以使用Visual Studio Code，甚至是任何壹個文本編輯器。我是使用 [Visual Studio Code](http://code.visualstudio.com/).

## 如何安裝模板

安裝這些模板非常簡單，妳只需要壹行命令即可，`dotnet new -i 模板標識`，例如使用`dotnet new -i chenxizhang.dotnetcore.msgraph.console.CSharp` 來安裝可以快速實現Microsoft Graph的控制臺應用程序模板。

## 如何使用模板

模板安裝成功後，妳可以在模板列表中看到這些新的模板，每個模板都有壹個短名稱（Short Name），妳可以通過 `dotnet new 模板短名稱`來使用這些模板，例如 `dotnet new graphconsole` 這句命令就可以快速基於模板創建壹個可以快速實現Microsoft Graph的控制臺應用程序。

每個模板都帶有壹些參數以便支持不同的場景，最典型的參數是 `--instance`，這個參數將告訴模板引擎，妳使用的Office 365環境是國際版還是國內版的，它是壹個必填項，有兩個選項，分別是`global`代表國際版，`gallatin`代表國內版，但默認會設置為國際版。

妳不需要記住所有這些參數，而是可以通過 `dotnet new 模板短名稱 -h`這樣的命令來查看該模板的介紹和參數說明。

每個模板都帶有壹個詳細的說明文檔，妳可以點擊上表中模板短名稱跳轉，同時在生成的代碼文件的頂部也有鏈接，我非常歡迎大家給我反饋。

## 如何卸載模板

在如下兩種情況下，妳可能需要卸載模板

1. 妳不再喜歡這些模板（我希望這種情況不會發生）
1. 妳需要安裝模板的更新版本

無論是哪壹種原因，妳都可以隨時簡單地通過 `dotnet new -u 模板標識` 進行卸載，例如`dotnet new -u chenxizhang.dotnetcore.msgraph.console.CSharp`可以卸載控制臺應用程序模板。請放心，我不會帶走壹片雲彩。

## 常見問題解答（將持續更新）

1. 這些模板支持哪些開發語言？
    >目前僅支持C#。限於能力和精力，其他的語言暫時沒有支持計劃，歡迎有其他語言特長（例如F#,VB.NET等）的朋友參與該項目。