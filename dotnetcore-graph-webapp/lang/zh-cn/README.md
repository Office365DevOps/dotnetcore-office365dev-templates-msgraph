# Microsoft Graph 网站应用程序模板

[English](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-webapp/README.md) | 简体中文 | [繁體中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-webapp/lang/zh-tw/README.md)

> 作者：陈希章 发表于 2018年4月28日

## 概述

这个项目模板可以帮助你快速建立一个网站，并且以某个用户的身份调用Microsoft Graph的服务，实现了如下三个功能

1. 读取当前用户的基本信息
1. 读取当前用户的邮箱收件箱（Exchange Online）中的前十封邮件
1. 读取当前用户的个人网盘（OneDrive for Business）的根目录下面的文件列表

该模板支持国际版，也支持国内版。

## 准备

为了使用该模板，你最好能自行注册一个应用程序。如果你对这方面的概念不熟悉，请参考 [Microsoft Graph overview](https://github.com/chenxizhang/office365dev/blob/master/docs/microsoftgraphoverview.md)的说明。

如果你是需要访问国际版Office 365，建议你直接使用AAD 2.0的注册方式，在<https://apps.dev.microsoft.com>进行注册，请参考[这篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration2.0.md)。

如果你是需要访问国内版Office 365，你目前只能使用AAD 1.0的注册方式，在<https://portal.azure.cn>进行注册，请参考[这篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration.md)。

这个模板范例，至少需要三个委派权限（Delegate Permission）

1. Files.Read.All
1. Mail.Read
1. User.Read

不管你采用哪种方式注册，请将Reply Url中设置为 <http://localhost:5000/signin-odic>

## 安装

你通过 `dotnet new -i chenxizhang.dotnetcore.msgraph.webapp.CSharp` 即可安装这个项目模板。

## 使用

这个模板有几个用法，分别如下

1. 最简单的用法 `dotnet new graphwebapp` 将创建一个模板实现，你将使用我预先创建好的一个clientid访问到Office 365国际版。
1. 通过指定`clientid`参数和`secret`参数，明确使用你的应用程序来访问Office 365， 这是我最推荐的，语法是 `dotnet new graphwebapp --clientid 你创建的应用程序编号 --secret 你的密钥`。
1. 通过`instance`参数，指定你要访问的是国际版还是国内版。国际版是默认的，而如果要指定国内版，则需要用如下的语法 `dotnet new graphwebapp --instance gallatin --clientid 你创建的应用程序编号 --secret 你的密钥`。
1. 通过`version`参数，指定你要访问的Graph API的版本，默认为`v1.0`，目前还支持`beta`。

另外还有两个通用参数

1. 通过指定`name` 可以改变模板生成的项目名称，以及默认的namespace名称。例如 `dotnet new graphwebapp -n mynamespace`。
1. 通过指定`output`可以指定生成新的项目目录。例如 `dotnet new graphwebapp -o test`。

一旦创建好项目，你可以直接通过`dotnet run`运行，或者在`Visual Studio Code`中编辑后再运行。你可以通过如下的地址访问三个默认的功能：

1. 读取当前用户的基本信息（<http://localhost:5000>)
1. 读取当前用户的邮箱收件箱（Exchange Online）中的前十封邮件（<http://localhost:5000/messages>)
1. 读取当前用户的个人网盘（OneDrive for Business）的根目录下面的文件列表（<http://localhost:5000/files>)

## 卸载

你可以通过 `dotnet new -u chenxizhang.dotnetcore.msgraph.webapp.CSharp`可以卸载当前这个项目模板。