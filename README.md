# .NET Core Templates for Office 365 Developer

English | [简体中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/lang/zh-cn/README.md) | [繁體中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/lang/zh-tw/README.md)

> Author：Ares Chen @ 2018/4/22

## Overview

I wrote an article not long ago [embrace open source, Office 365 development ushered in new age] (https://github.com/chenxizhang/office365dev/blob/master/docs/office365opensource.md), to show you how the typical scenario of Office 365 development supports the open source platform: Use Microsoft Graph, Any development platform can be easily integrated with Office 365; Office Add-ins, SharePoint Add-ins, and Microsoft Teams Apps are developed based on a standard Web development technology stack, and the official default support for mainstream platforms and frameworks such as NodeJS, React, TypeScript, and so on at the tool and template level.

After the publication of the article, I received quite a lot of feedback. In fact, I am deeply concerned about the vast number of.NET developers. As you know,.NET is completely open - source from head to foot. Is there any templates for Office 365 development scenarios for everyone to use? Using the weekend, I started this new project to organize a standard.NET Core template library for you, and welcome your most familiar position to <https://github.com/chenxizhang/dotnetcore-office365dev-templates>. You can simply and directly give me `star`, or you can `fork` the project, you can also submit your template through `pull request`, or give me feedback through `issue` channel.

This set of template libraries will not only reduce the threshold of the vast number of.NET developers (exactly as.NET Core developers) to learn and use the Office 365 development platform (especially how to complete OAuth authentication in different scenarios, and to learn the typical functions of Graph quickly through an instance), and there is a unique value ——  You can easily switch between the two instance of Office 365 : the `global` instance and the `gallatin` instance. 

enjoy and happy coding!

## The templates

|Name|Identifier|Short Name|Global|Gallatin|
|:---|:---|:---|:---|:---|
|`Console Application(Graph)`|chenxizhang.<br />dotnetcore.<br />msgraph.<br />console.<br />CSharp|[graphconsole](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-console/README.md)|Yes|Yes|
|`Daemon Application(Graph)`|chenxizhang.<br />dotnetcore.<br />msgraph.<br />serviceapp.<br />CSharp|[graphserviceapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-serviceapp/README.md)|Yes|Yes|
|`ASP.NET Web Application(Graph)`|chenxizhang.<br />dotnetcore.<br />msgraph.<br />webapp.<br />CSharp|[graphwebapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-webapp/README.md)|Yes|Yes|
|`ASP.NET MVC Application(Graph)`|chenxizhang.<br />dotnetcore.<br />msgraph.<br />mvcapp.<br />CSharp|[graphmvcapp](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-mvcapp/README.md)|Yes|Yes|
|`ASP.NET Web API (Graph)`|chenxizhang.<br />dotnetcore.<br />msgraph.<br />mvcapi.<br />CSharp|[graphwebapi](https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-webapi/README.md)|Yes|Yes|

## Prerequisites

To use these templates, you only need to install.NET Core SDK version 2 or above on the development machine. Please download and install it via the official website (<https://www.microsoft.com/net/download/>), Please note that we are now cross platform, whether Windows, or Mac, or Linux support.NET Core development, while.NET Core applications can be deployed with `docker`, and its development and deployment, operation processes combined with `devops` are also silky.

In addition, you may need at least one code editor, you can install Visual Studio, or Visual Studio Code, or even any text editor. I use [Visual Studio Code] (http://code.visualstudio.com/).

## How to install

Installing these templates is very simple, you just need a line of commands, `dotnet new -i template-identifier`, for example, using `dotnet new -i chenxizhang.dotnetcore.msgraph.console.CSharp` to install a console application template that can quickly implement Microsoft Graph.

## How to use the template

After the template installation is successful, you can see these new templates in the template list, each template has a short name, you can use the `dotnet new template-shortname` to use these templates, such as `dotnet new graphconsole`, the command can quickly build a Microsoft Graph console application.

Each template has some parameters to support different scenarios. The most typical parameter is `--instance`. This parameter will tell the template engine which office 365 environment you want to use. There are two options, `global` for the international version, and `gallatin` for the domestic version for China. `gloal` will be the default value for this parameter.

Actully, you don't need to memorize all these parameters, and you can see the introduction and parameter description of the template through the commands such as `dotnet new template-shortname -h`.

Each template has a detailed description document. You can click on the shortname of the template in the upper table, and also have a link at the top of the generated code file. I very much welcome you to give me feedback.

## How to uninstall the template

In the following two cases, you may need to uninstall the template.
1. you don't like these templates anymore.
1. you need to install the new version of the template

Whatever the reason, you can simply uninstall through the `dotnet new -u template-identifier` at any time, for example, use `dotnet new -u chenxizhang.dotnetcore.msgraph.console.CSharp` can uninstall the console application template. 

## FAQ

1. What languages do these templates support?
    >Currently only C# is supported. Limited to ability and energy, other languages have no support plan for the time being. Welcome friends with other language skills (such as F#, VB.NET, etc.) to participate in the project.