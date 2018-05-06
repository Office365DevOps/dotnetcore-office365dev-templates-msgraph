# Microsoft Graph 服务应用程序模板

[English](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-serviceapp/README.md) | 简体中文 | [繁體中文](https://github.com/chenxizhang/dotnetcore-office365dev-templates/blob/master/dotnetcore-graph-serviceapp/lang/zh-tw/README.md)

> 作者：陈希章 发表于 2018年4月22日

## 概述

服务应用程序指的是不需要用户交互的一类应用程序，他们很可能是通过定时器在后台运行。由于这种特性，它们在访问Microsoft Graph时往往不是以某个特定的用户身份，而是用一个固定的身份来完成。在这个项目模板中，我们实现的是Oauth 2.0中标准的Client Credential 这种认证流程，即应用程序使用自己的应用程序编号（`clientid`），和一个密钥（`secret`）来表明自己的身份，而它获得的权限，在得到Office 365 管理员确认后，可以访问任何用户的数据。

该模板实现了五个基本功能

1. 读取所有用户信息
1. 读取某个用户的收件箱邮件列表
1. 读取某个用户的个人网盘根目录的文件列表
1. 代表某个用户发送邮件
1. 给某个用户的个人网盘上传一个文本文件

## 准备

为了使用该模板，你最好能自行注册一个应用程序。如果你对这方面的概念不熟悉，请参考 [Microsoft Graph overview](https://github.com/chenxizhang/office365dev/blob/master/docs/microsoftgraphoverview.md)的说明。

如果你是需要访问国际版Office 365，建议你直接使用AAD 2.0的注册方式，在<https://apps.dev.microsoft.com>进行注册，请参考[这篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration2.0.md)。

如果你是需要访问国内版Office 365，你目前只能使用AAD 1.0的注册方式，在<https://portal.azure.cn>进行注册，请参考[这篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration.md)。

请在注册应用程序后，生成一个新的密钥（secret），并且复制保存起来。

这个模板范例，至少需要三个应用权限（Application Permission）

1. Files.Read.All（Admin）
1. Mail.Read（Admin）
1. User.Read.All(Admin)
1. Mail.Send(Admin)
1. File.ReadWrite.All(Admin)

请注意，这一类应用权限，需要得到Office 365管理的确认（Admin consent），项目生成后，还会有这方面的操作提示。

## 安装

你通过 `dotnet new -i chenxizhang.dotnetcore.msgraph.serviceapp.CSharp` 即可安装这个项目模板。

## 使用

这个项目模板定义了五个参数，分别如下

1. `instance`，用来定义目标Office 365环境的版本，`global`代表国际版（默认），`gallatin`代表国内版。
1. `clientid`,注册应用程序时获得的应用程序编号。
1. `secret`,注册应用程序时生成的密钥。
1. `tenantid`，指定你要最终访问的租户id。不同于委派权限，应用程序权限都必须要明确地限定到具体的租户，而不是多租户的。这个tenantid有几种方式获取，一种是你在做Admin consent的时候，在返回结果的地址栏中有这个信息。你也可以通过[我的这篇文章](http://www.cnblogs.com/chenxizhang/p/7904293.html)所介绍的PowerShell方法获取。
1. `version`，指定你要访问的Graph API的版本，默认为`v1.0`，目前还支持`beta`。

如果你还不太清楚怎么注册应用程序，以及如何做Admin consent，你可以直接运行最基本的一个命令`dotnet new graphserviceapp`，使用我配置好的一个环境体验一下这几个功能。
> 我的这个测试环境经常会修改，而且可能会过期，所以可能你在用的时候会遇到无法正常工作的情况。

该模板的常规使用方法是至少指定前四个参数，如 `dotnet new graphserviceapp --instance gallatin --clientid 你的应用程序编号 --secret 你的密钥 --tenantid 你的租户编号`。

另外还有两个通用参数

1. 通过指定`name` 可以改变模板生成的项目名称，以及默认的namespace名称。例如 `dotnet new graphserviceapp -n myserviceapp`。
1. 通过指定`output`可以指定生成新的项目目录。例如 `dotnet new graphserviceapp -o testapp`。

另外，请注意，项目生成后你不能直接就`dotnet run`进行运行，而是至少修改两行代码（第72，73行），设置你要访问的用户upn和邮件收件人信息。

## 卸载

你可以通过 `dotnet new -u chenxizhang.dotnetcore.msgraph.serviceapp.CSharp`可以卸载当前这个项目模板。