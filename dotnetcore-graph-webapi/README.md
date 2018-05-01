# Microsoft Graph Web API 模板

> 作者：陈希章 发表于 2018年5月1日

## 概述

这个项目模板可以帮助你快速建立一个服务项目，并且可以接受客户端调用，通过On-Behalf-Of认证流实现以某个用户的身份调用Microsoft Graph的服务，实现了如下一个功能

1. 读取当前用户的基本信息

该模板支持国际版，也支持国内版。

## 准备

为了使用该模板，你最好能自行注册一个应用程序。如果你对这方面的概念不熟悉，请参考 [Microsoft Graph overview](https://github.com/chenxizhang/office365dev/blob/master/docs/microsoftgraphoverview.md)的说明。

不管你是使用国际版还是国内版，你目前只能使用AAD 1.0的注册方式，在<https://portal.azure.cn>进行注册，请参考[这篇文章](https://github.com/chenxizhang/office365dev/blob/master/docs/applicationregisteration.md)。

这个模板范例，至少需要一个委派权限（Delegate Permission）

1. User.Read

这个模板实现了一套完整的On-Behalf-Of认证流，即客户端应用程序先自行从Azure AD中得到访问许可，然后再调用中间层服务的时候，服务层会使用客户端的Access_Token去换取自己的Access_Token，看起来就好像客户端自己去访问一样。关于这个场景，以及如何配置On-Behalf-Of的应用程序和权限，请参考 <https://github.com/chenxizhang/active-directory-dotnet-webapi-onbehalfof> 这里的说明。你需要注册两个应用程序。


## 安装

你通过 `dotnet new -i chenxizhang.dotnetcore.msgraph.webapi.CSharp` 即可安装这个项目模板。

## 使用

这个模板有几个用法，分别如下

1. 最简单的用法 `dotnet new graphwebapi` 将创建一个模板实现，你将使用我预先创建好的一个clientid访问到Office 365国际版。
1. 通过指定`clientid`参数和`secret`参数、`tenantid`参数、`obo-console-clientid`参数，明确使用你的应用程序来访问Office 365， 这是我最推荐的，语法是 `dotnet new graphwebapi --clientid 你创建的应用程序编号 --secret 你的密钥 --tenantid 你的租户编号 --obo-console-clientid 你为客户端注册的应用程序编号`。关于如何获取`tenantid`,你也可以通过[我的这篇文章](http://www.cnblogs.com/chenxizhang/p/7904293.html)所介绍的PowerShell方法获取。
1. 通过`instance`参数，指定你要访问的是国际版还是国内版。国际版是默认的，而如果要指定国内版，则需要用如下的语法 `dotnet new graphwebapi --instance gallatin --clientid 你创建的应用程序编号 --secret 你的密钥`。
1. 通过`version`参数，指定你要访问的Graph API的版本，默认为`v1.0`，目前还支持`beta`。

另外还有两个通用参数

1. 通过指定`name` 可以改变模板生成的项目名称，以及默认的namespace名称。例如 `dotnet new graphwebapi -n mynamespace`。
1. 通过指定`output`可以指定生成新的项目目录。例如 `dotnet new graphwebapi -o test`。

一旦创建好项目，你可以直接通过`dotnet run`运行，或者在`Visual Studio Code`中编辑后再运行。

1. 读取当前用户的基本信息

## 卸载

你可以通过 `dotnet new -u chenxizhang.dotnetcore.msgraph.webapi.CSharp`可以卸载当前这个项目模板。