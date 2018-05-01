# Office 365 应用开发的 .NET Core 模板库

> 作者：陈希章 发表于 2018年4月22日

## 概述

前不久我写过一篇文章[拥抱开源，Office 365开发迎来新时代](https://github.com/chenxizhang/office365dev/blob/master/docs/office365opensource.md)，给大家介绍了Office 365开发的典型场景是如何支持开源平台的：Office 365通过Microsoft Graph，以REST API的方式提供服务，任何开发平台都可以很方便地与其进行集成；Office Add-ins，SharePoint Add-ins和Microsoft Teams Apps开发，都可以基于标准的Web开发技术栈来实现，而且官方提供了对NodeJS，React，TypeScript等主流平台和框架的默认支持（工具和模板层面都有）。

文章发表之后，我收到不少反馈，其实我内心深处牵挂的还有广大的.NET开发人员啊。大家知道，.NET从头到脚都是已经完全开源了的，针对Office 365的这些开发场景，是否有开箱即用的模板可供大家使用呢？利用周末的时间，我开始了这个新的项目，就是为大家整理出来一套标准的.NET Core模板库，欢迎用你最熟悉的姿势关注 <https://github.com/chenxizhang/dotnetcore-office365dev-templates>，你可以简单粗暴地直接给我 `star`,也可以 `fork` 这个项目通过 `pull request` 提交你的模板，你还可以通过`issue`通道给我反馈问题。

这一套模板库，不仅仅可以降低广大的.NET 开发人员（准确地说是.NET Core开发人员）学习和使用Office 365开发平台的门槛（尤其是如何在不同的场景下完成OAuth认证以及快速通过实例学习Graph的典型功能），还有一个独特的价值是可以让你在国际版和国内版这两个平台的选择和切换过程中少走一些弯路，我相信真正做过这方面开发的朋友们现在一定是热泪盈眶的表情。

希望大家喜欢！

## 计划支持的模板

|模板|模板标识|模板短名称|国际版|国内版|
|:---|:---|:---|:---|:---|
|控制台应用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />console.<br />CSharp|[graphconsole](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-console/README.md)|支持|支持|
|无人值守应用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />serviceapp.<br />CSharp|[graphserviceapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-serviceapp/README.md)|支持|支持|
|ASP.NET Web应用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />webapp.<br />CSharp|[graphwebapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-webapp/README.md)|支持|支持|
|ASP.NET MVC应用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />mvcapp.<br />CSharp|[graphmvcapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-mvcapp/README.md)|支持|支持|
|ASP.NET Web API 应用程序(Graph)|chenxizhang.<br />dotnetcore.<br />msgraph.<br />mvcapi.<br />CSharp|[graphwebapi](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-webapi/README.md)|支持|支持|
|Office Add-ins （Excel）|即将提供|即将提供|支持|支持|
|SharePoint Add-ins |即将提供|即将提供|支持|支持|
|Microsoft Teams Apps |即将提供|即将提供|支持|暂不支持|

## 先决条件

要使用这些模板，你只需要在开发机器上面安装了 .NET Core SDK 版本2.0 及以上即可。请通过官方网站提示到的方式进行下载和安装(<https://www.microsoft.com/net/download/>)。请注意，我们现在是跨平台的哦，无论Windows，还是Mac，或是Linux都支持进行.NET Core开发，与此同时, .NET Core应用程序可以使用`docker`进行部署，其开发和部署、运营流程结合`devops`也是如丝般润滑的。

另外，你可能至少需要一款代码编辑器，你可以安装Visual Studio，也可以使用Visual Studio Code，甚至是任何一个文本编辑器。我是使用 [Visual Studio Code](http://code.visualstudio.com/).

## 如何安装模板

安装这些模板非常简单，你只需要一行命令即可，`dotnet new -i 模板标识`，例如使用`dotnet new -i chenxizhang.dotnetcore.msgraph.console.CSharp` 来安装可以快速实现Microsoft Graph的控制台应用程序模板。

## 如何使用模板

模板安装成功后，你可以在模板列表中看到这些新的模板，每个模板都有一个短名称（Short Name），你可以通过 `dotnet new 模板短名称`来使用这些模板，例如 `dotnet new graphconsole` 这句命令就可以快速基于模板创建一个可以快速实现Microsoft Graph的控制台应用程序。

每个模板都带有一些参数以便支持不同的场景，最典型的参数是 `--instance`，这个参数将告诉模板引擎，你使用的Office 365环境是国际版还是国内版的，它是一个必填项，有两个选项，分别是`global`代表国际版，`gallatin`代表国内版，但默认会设置为国际版。

你不需要记住所有这些参数，而是可以通过 `dotnet new 模板短名称 -h`这样的命令来查看该模板的介绍和参数说明。

每个模板都带有一个详细的说明文档，你可以点击上表中模板短名称跳转，同时在生成的代码文件的顶部也有链接，我非常欢迎大家给我反馈。

## 如何卸载模板

在如下两种情况下，你可能需要卸载模板

1. 你不再喜欢这些模板（我希望这种情况不会发生）
1. 你需要安装模板的更新版本

无论是哪一种原因，你都可以随时简单地通过 `dotnet new -u 模板标识` 进行卸载，例如`dotnet new -u chenxizhang.dotnetcore.msgraph.console.CSharp`可以卸载控制台应用程序模板。请放心，我不会带走一片云彩。

## 常见问题解答（将持续更新）

1. 这些模板支持哪些开发语言？
    >目前仅支持C#。限于能力和精力，其他的语言暂时没有支持计划，欢迎有其他语言特长（例如F#,VB.NET等）的朋友参与该项目。