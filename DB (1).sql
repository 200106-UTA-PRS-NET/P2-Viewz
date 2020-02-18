USE [master]
GO
/****** Object:  Database [ViewzDB]    Script Date: 2020-02-15 17:54:15 ******/
CREATE DATABASE [ViewzDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ViewzDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ViewzDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ViewzDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ViewzDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ViewzDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ViewzDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ViewzDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ViewzDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ViewzDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ViewzDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ViewzDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ViewzDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ViewzDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ViewzDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ViewzDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ViewzDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ViewzDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ViewzDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ViewzDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ViewzDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ViewzDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ViewzDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ViewzDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ViewzDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ViewzDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ViewzDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ViewzDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ViewzDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ViewzDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ViewzDB] SET  MULTI_USER 
GO
ALTER DATABASE [ViewzDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ViewzDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ViewzDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ViewzDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ViewzDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ViewzDB] SET QUERY_STORE = OFF
GO
USE [ViewzDB]
GO
/****** Object:  Schema [wiki]    Script Date: 2020-02-15 17:54:15 ******/
CREATE SCHEMA [wiki]
GO
/****** Object:  Table [wiki].[contents]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[contents](
	[pageId] [bigint] NOT NULL,
	[content] [nvarchar](255) NOT NULL,
	[id] [nvarchar](255) NOT NULL,
	[order] [int] NOT NULL,
	[level] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [wiki].[images]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[images](
	[wikiId] [int] NOT NULL,
	[imageName] [nvarchar](255) NOT NULL,
	[image] [varbinary](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wiki].[page]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[page](
	[wikiId] [int] NOT NULL,
	[pageId] [bigint] NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[PageName] [ntext] NULL,
	[hitCount] [bigint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wiki].[pageDetails]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[pageDetails](
	[pageId] [bigint] NOT NULL,
	[detKey] [nvarchar](255) NOT NULL,
	[detValue] [nvarchar](255) NULL,
	[order] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [wiki].[pageHtmlContent]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[pageHtmlContent](
	[pageId] [bigint] NOT NULL,
	[HtmlContent] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wiki].[pageMdContent]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[pageMdContent](
	[pageId] [bigint] NOT NULL,
	[MdContent] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wiki].[wiki]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[wiki](
	[Id] [int] NOT NULL,
	[Url] [nvarchar](255) NOT NULL,
	[PageName] [ntext] NULL,
	[hitCount] [bigint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wiki].[wikiHtmlDescription]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[wikiHtmlDescription](
	[wikiId] [int] NOT NULL,
	[HtmlDescription] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wiki].[wikiMdDescription]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wiki].[wikiMdDescription](
	[wikiId] [int] NOT NULL,
	[MdDescription] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (1, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (1, N'training-code', N'user-content-training-code', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (1, N'Environment Setup', N'user-content-environment-setup', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (1, N'tools:', N'user-content-tools', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (1, N'gitignore', N'user-content-gitignore', 4, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (1, N'Useful Links', N'user-content-useful-links', 5, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (2, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (2, N'Entity Framework Core Steps:', N'user-content-entity-framework-core-steps', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (2, N'Package Manager Console (for VS2019 users)', N'user-content-package-manager-console-for-vs2019-users', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (2, N'.NET core CLI COMMANDS (specially VSCODE users)', N'user-content-net-core-cli-commands-specially-vscode-users', 3, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (2, N'To move the connection string from generated dbcontext file to JSON configuration file:', N'user-content-to-move-the-connection-string-from-generated-dbcontext-file-to-json-configuration-file', 4, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (3, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (3, N'Super Fun Times', N'user-content-super-fun-times', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (4, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (4, N'Super Fun Times', N'user-content-super-fun-times', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (4, N'H2s are fun too', N'user-content-h2s-are-fun-too', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (6, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (6, N'This is a super cool page', N'user-content-this-is-a-super-cool-page', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (6, N'H2s are important for further categorization of information', N'user-content-h2s-are-important-for-further-categorization-of-information', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (7, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (7, N'This is a super cool page2', N'user-content-this-is-a-super-cool-page2', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (7, N'H2s are important for further categorization of information', N'user-content-h2s-are-important-for-further-categorization-of-information', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (8, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (8, N'This is a super cool page3', N'user-content-this-is-a-super-cool-page3', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (8, N'H2s are important for further categorization of information', N'user-content-h2s-are-important-for-further-categorization-of-information', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (8, N'H3s are good too', N'user-content-h3s-are-good-too', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (9, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (9, N'This is a test page to see why it''s giving me 500', N'user-content-this-is-a-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (10, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (10, N'This is a 2 test page to see why it''s giving me 500', N'user-content-this-is-a-2-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (11, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (11, N'This is a 3 test page to see why it''s giving me 500', N'user-content-this-is-a-3-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (12, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (12, N'This is a 4 test page to see why it''s giving me 500', N'user-content-this-is-a-4-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (13, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (13, N'This is a 5 test page to see why it''s giving me 500', N'user-content-this-is-a-5-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (15, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (15, N'This is a 6 test page to see why it''s giving me 500', N'user-content-this-is-a-6-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (16, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (16, N'This is a 7 test page to see why it''s giving me 500', N'user-content-this-is-a-7-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (17, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (17, N'This is a 8 test page to see why it''s giving me 500', N'user-content-this-is-a-8-test-page-to-see-why-its-giving-me-500', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (18, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (18, N'Page Updated From Put Request', N'user-content-page-updated-from-put-request', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (18, N'This is an H2', N'user-content-this-is-an-h2', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (18, N'This is an H3', N'user-content-this-is-an-h3', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (19, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (19, N'Page Test 2', N'user-content-page-test-2', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (19, N'This is an H2', N'user-content-this-is-an-h2', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (19, N'This is an H3', N'user-content-this-is-an-h3', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (20, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (20, N'Test header 1', N'user-content-test-header-1', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (20, N'Test header 2', N'user-content-test-header-2', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (20, N'Test header 3', N'user-content-test-header-3', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (21, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (21, N'Test header 1', N'user-content-test-header-1', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (21, N'Test header 2', N'user-content-test-header-2', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (21, N'Test header 3', N'user-content-test-header-3', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (22, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (22, N'Page Test 3', N'user-content-page-test-3', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (22, N'This is an H2', N'user-content-this-is-an-h2', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (22, N'This is an H3', N'user-content-this-is-an-h3', 3, 3)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (23, N'link', N'bad', 0, 0)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (23, N'Page Test 4 Modified from PATCH', N'user-content-page-test-4-modified-from-patch', 1, 1)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (23, N'This is an H2', N'user-content-this-is-an-h2', 2, 2)
INSERT [wiki].[contents] ([pageId], [content], [id], [order], [level]) VALUES (23, N'This is an H3', N'user-content-this-is-an-h3', 3, 3)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 1, N'readme', N'ReadMe', 7)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 2, N'efcoresteps', N'EF Core Steps', 16)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 3, N'new-page', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 4, N'new-page2', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 6, N'super-new-page-nobody-has-thought-of', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 7, N'super-new-page-nobody-has-thought-of2', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 8, N'super-new-page-nobody-has-thought-of3', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 9, N'this-is-a-page', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 10, N'this-is-a-page2', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 11, N'this-is-a-page3', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 12, N'this-is-a-page4', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 13, N'this-is-a-page5', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 15, N'this-is-a-page6', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 16, N'this-is-a-page7', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 17, N'this-is-a-page8', N'Test Page Name', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 18, N'some-page-test', N'Page Name Test', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 19, N'some-page-test2', N'some-page-test2', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 20, N'aspofnsajdfn', N'aspofnsajdfn', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 21, N'aosijfoawhilskndv', N'aosijfoawhilskndv', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 22, N'some-page-test3', N'some-page-test3', 0)
INSERT [wiki].[page] ([wikiId], [pageId], [Url], [PageName], [hitCount]) VALUES (1, 23, N'some-page-test4', N'some-page-test4', 0)
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (1, N'<h1>
<a id="user-content-training-code" class="anchor" href="#training-code" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>training-code</h1>
<p>This repository will be used for sharing code, projects and notes</p>
<h2>
<a id="user-content-environment-setup" class="anchor" href="#environment-setup" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Environment Setup</h2>
<ul>
<li>
<a href="https://github.com">github</a>
<ul>
<li>Create an account</li>
<li>We will use this for version control, class examples, and submission of your assignments</li>
</ul>
</li>
<li>
<a href="https://git-scm.com/downloads" rel="nofollow">git for windows + git bash</a>
<ul>
<li>installs linux-like bash environment (terminal)</li>
<li>also installs git, for version control</li>
</ul>
</li>
<li>
<a href="https://slack.com" rel="nofollow">Slack</a>
<ul>
<li><a href="http://www.slack.com" rel="nofollow">www.slack.com</a></li>
<li>Create a slack account or join using the <a href="https://join.slack.com/t/revaturepro/shared_invite/enQtODcyNzMxNTAyOTAwLTI2ODI4ZTVhY2E4YTgwMjYzZDczNTkyNDVhZTJkZjExNjlkMGNlNTE3MDY3NzBiZjk5OGQxOTczZDE1MWM5Mzg" rel="nofollow">magic link</a>.</li>
<li>We will use this for communications between the group outside of work hours.</li>
</ul>
</li>
</ul>
<h3>
<a id="user-content-tools" class="anchor" href="#tools" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>tools:</h3>
<ul>
<li>
<a href="https://visualstudio.microsoft.com/downloads/" rel="nofollow">visual studio</a>
<ul>
<li>atleast .net core workload required for week 1</li>
</ul>
</li>
<li><a href="https://code.visualstudio.com/download" rel="nofollow">visual studio code</a></li>
<li>
<a href="https://dotnet.microsoft.com/download" rel="nofollow">.net core sdk</a>
<ul>
<li>lets us compile c# code.</li>
<li>included with visual studio workload</li>
<li>gives us "dotnet" command</li>
</ul>
</li>
</ul>
<h3>
<a id="user-content-gitignore" class="anchor" href="#gitignore" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a><a href="https://github.com/dotnet/core/blob/master/.gitignore">gitignore</a>
</h3>
<h3>
<a id="user-content-useful-links" class="anchor" href="#useful-links" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Useful Links</h3>
<ul>
<li><a href="https://www.git-tower.com/blog/git-cheat-sheet" rel="nofollow">Git Cheat Sheet</a></li>
<li><a href="https://youtu.be/0fKg7e37bQE" rel="nofollow">Git Basics</a></li>
<li><a href="https://youtu.be/oFYyTZwMyAg" rel="nofollow">Git Team Basics</a></li>
</ul>
<p><em>The most common laptops are Windows PCs. Where MacOS and Linux systems can use package managers, Windows prefers its own GUI wizards.</em></p>
<ul>
<li>
<a href="https://www.hackerrank.com/" rel="nofollow">Hacker Rank</a>
<ul>
<li>Good source of practice. Use it often for practice. Of course, if you still have assigned work to do, that work takes precedence.</li>
</ul>
</li>
<li>
<a href="https://guides.github.com/features/mastering-markdown/">learn about md files</a>
<ul>
<li>it''s always good to read and manage markdowns.</li>
<li>Also <a href="https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#headers">markdown cheatsheet</a>
</li>
</ul>
</li>
</ul>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (2, N'<h1>
<a id="user-content-entity-framework-core-steps" class="anchor" href="#entity-framework-core-steps" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Entity Framework Core Steps:</h1>
<h2>
<a id="user-content-package-manager-console-for-vs2019-users" class="anchor" href="#package-manager-console-for-vs2019-users" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Package Manager Console (for VS2019 users)</h2>
<ul>
<li>Run following commands in your Data Layer project in the VS2019 PMC (in your Tools menu option-&gt;Nuget Package Manager -&gt; Package Manager Console)</li>
<li><code>Install-Package Microsoft.EntityFrameworkCore.SqlServer</code></li>
<li><code>Install-Package Microsoft.EntityFrameworkCore.Tools</code></li>
<li><code>Install-Package Microsoft.EntityFrameworkCore.Design</code></li>
<li>Build the project before Scaffolding</li>
<li>
<code>Get-Help about_EntityFrameworkCore</code> - See the EF help commands</li>
<li>
<code>Scaffold-DbContext -connection "Server=&lt;DB Server Name&gt;;Database=&lt;DB Name&gt;;user id=&lt;username&gt;;Password=&lt;password&gt;;" -provider Microsoft.EntityFrameworkCore.SqlServer -outputDir &lt;Output Directory name&gt; -context &lt;context name&gt;</code> - Its is mandatory to provide connectio string and the provider rest parameters are optional</li>
</ul>
<h2>
<a id="user-content-net-core-cli-commands-specially-vscode-users" class="anchor" href="#net-core-cli-commands-specially-vscode-users" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>.NET core CLI COMMANDS (specially VSCODE users)</h2>
<ul>
<li>Run following commands in your Data Layer project in the vscode terminal/gitbash/Command prompt)</li>
<li><code>dotnet add package Microsoft.EntityFrameworkCore.SqlServer</code></li>
<li><code>dotnet add package Microsoft.EntityFrameworkCore.Design</code></li>
<li><code>dotnet add package Microsoft.EntityFrameworkCore.Tool</code></li>
<li>
<code>dotnet ef</code> - to check EF is installed</li>
<li>
<code>dotnet ef -h</code> -See the EF help commands</li>
<li>
<code>dotnet ef dbcontext scaffold -h</code> - see if help about EF Db Context Scaffold commands</li>
<li>
<code>dotnet ef dbcontext scaffold -connection "Server=&lt;server name&gt;;Database=&lt;Db Name&gt;;user id=&lt;username&gt;;Password=&lt;password&gt;;" -provider Microsoft.EntityFrameworkCore.SqlServer -outputDir &lt;Output Directory name&gt; -context &lt;context name&gt;</code> - to get all Entities mapped from database objects to C# classes.</li>
</ul>
<p><a href="https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx" rel="nofollow">Reference</a></p>
<h2>
<a id="user-content-to-move-the-connection-string-from-generated-dbcontext-file-to-json-configuration-file" class="anchor" href="#to-move-the-connection-string-from-generated-dbcontext-file-to-json-configuration-file" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>To move the connection string from generated dbcontext file to JSON configuration file:</h2>
<ol>
<li>add package <code>Microsoft.Extensions.Configuration.Json</code> to the console app. (turns out the other two packages from before are dependencies that are automatically added)</li>
<li>add new item to console app project, type "JSON File", named "appsettings.json", with these contents:</li>
</ol>
<pre><code>{
  "ConnectionStrings": {
    "&lt;name you give to this data source&gt;": "&lt;your connection string&gt;"
  }
}
</code></pre>
<ol start="3">
<li>right-click on "appsettings.json" in the solution explorer, go to Properties, change "Copy to Output Directory" to "Copy if newer".</li>
<li>make a ".gitignore" file in your solution directory, with the contents "appsettings.json".</li>
<li>in <code>Program.Main</code> of your console app, add the code:</li>
</ol>
<pre><code>var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = configBuilder.Build();
</code></pre>
<ol start="6">
<li>to get the options ready to make a dbcontext, use the code (substituting the right stuff):</li>
</ol>
<pre><code>var optionsBuilder = new DbContextOptionsBuilder&lt;YourAppDbContext&gt;();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("&lt;name you gave to the data source&gt;"));
var options = optionsBuilder.Options;
</code></pre>
<ol start="7">
<li>then, when you need to make a dbcontext (to give you to your repository classes): <code>new YourAppDbContext(options)</code>.</li>
</ol>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (3, N'<h1>
<a id="user-content-super-fun-times" class="anchor" href="#super-fun-times" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Super Fun Times</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (4, N'<h1>
<a id="user-content-super-fun-times" class="anchor" href="#super-fun-times" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Super Fun Times</h1>
<h2>
<a id="user-content-h2s-are-fun-too" class="anchor" href="#h2s-are-fun-too" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>H2s are fun too</h2>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (6, N'<h1>
<a id="user-content-this-is-a-super-cool-page" class="anchor" href="#this-is-a-super-cool-page" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a super cool page</h1>
<h2>
<a id="user-content-h2s-are-important-for-further-categorization-of-information" class="anchor" href="#h2s-are-important-for-further-categorization-of-information" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>H2s are important for further categorization of information</h2>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (7, N'<h1>
<a id="user-content-this-is-a-super-cool-page2" class="anchor" href="#this-is-a-super-cool-page2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a super cool page2</h1>
<h2>
<a id="user-content-h2s-are-important-for-further-categorization-of-information" class="anchor" href="#h2s-are-important-for-further-categorization-of-information" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>H2s are important for further categorization of information</h2>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (8, N'<h1>
<a id="user-content-this-is-a-super-cool-page3" class="anchor" href="#this-is-a-super-cool-page3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a super cool page3</h1>
<h2>
<a id="user-content-h2s-are-important-for-further-categorization-of-information" class="anchor" href="#h2s-are-important-for-further-categorization-of-information" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>H2s are important for further categorization of information</h2>
<h3>
<a id="user-content-h3s-are-good-too" class="anchor" href="#h3s-are-good-too" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>H3s are good too</h3>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (9, N'<h1>
<a id="user-content-this-is-a-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (10, N'<h1>
<a id="user-content-this-is-a-2-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-2-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 2 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (11, N'<h1>
<a id="user-content-this-is-a-3-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-3-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 3 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (12, N'<h1>
<a id="user-content-this-is-a-4-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-4-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 4 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (13, N'<h1>
<a id="user-content-this-is-a-5-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-5-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 5 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (15, N'<h1>
<a id="user-content-this-is-a-6-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-6-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 6 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (16, N'<h1>
<a id="user-content-this-is-a-7-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-7-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 7 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (17, N'<h1>
<a id="user-content-this-is-a-8-test-page-to-see-why-its-giving-me-500" class="anchor" href="#this-is-a-8-test-page-to-see-why-its-giving-me-500" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is a 8 test page to see why it''s giving me 500</h1>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (18, N'<h1>
<a id="user-content-page-updated-from-put-request" class="anchor" href="#page-updated-from-put-request" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Page Updated From Put Request</h1>
<h2>
<a id="user-content-this-is-an-h2" class="anchor" href="#this-is-an-h2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H2</h2>
<h3>
<a id="user-content-this-is-an-h3" class="anchor" href="#this-is-an-h3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H3</h3>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (19, N'<h1>
<a id="user-content-page-test-2" class="anchor" href="#page-test-2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Page Test 2</h1>
<h2>
<a id="user-content-this-is-an-h2" class="anchor" href="#this-is-an-h2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H2</h2>
<h3>
<a id="user-content-this-is-an-h3" class="anchor" href="#this-is-an-h3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H3</h3>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (20, N'<h1>
<a id="user-content-test-header-1" class="anchor" href="#test-header-1" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Test header 1</h1>
<h2>
<a id="user-content-test-header-2" class="anchor" href="#test-header-2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Test header 2</h2>
<h3>
<a id="user-content-test-header-3" class="anchor" href="#test-header-3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Test header 3</h3>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (21, N'<h1>
<a id="user-content-test-header-1" class="anchor" href="#test-header-1" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Test header 1</h1>
<h2>
<a id="user-content-test-header-2" class="anchor" href="#test-header-2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Test header 2</h2>
<h3>
<a id="user-content-test-header-3" class="anchor" href="#test-header-3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Test header 3</h3>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (22, N'<h1>
<a id="user-content-page-test-3" class="anchor" href="#page-test-3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Page Test 3</h1>
<h2>
<a id="user-content-this-is-an-h2" class="anchor" href="#this-is-an-h2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H2</h2>
<h3>
<a id="user-content-this-is-an-h3" class="anchor" href="#this-is-an-h3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H3</h3>
')
INSERT [wiki].[pageHtmlContent] ([pageId], [HtmlContent]) VALUES (23, N'<h1>
<a id="user-content-page-test-4-modified-from-patch" class="anchor" href="#page-test-4-modified-from-patch" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Page Test 4 Modified from PATCH</h1>
<h2>
<a id="user-content-this-is-an-h2" class="anchor" href="#this-is-an-h2" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H2</h2>
<h3>
<a id="user-content-this-is-an-h3" class="anchor" href="#this-is-an-h3" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>This is an H3</h3>
')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (1, N'# training-code
This repository will be used for sharing code, projects and notes

## Environment Setup
* [github](https://github.com)
  * Create an account
  * We will use this for version control, class examples, and submission of your assignments
* [git for windows + git bash](https://git-scm.com/downloads) 
     * installs linux-like bash environment (terminal)
     * also installs git, for version control
* [Slack](https://slack.com)
  * www.slack.com
  * Create a slack account or join using the [magic link](https://join.slack.com/t/revaturepro/shared_invite/enQtODcyNzMxNTAyOTAwLTI2ODI4ZTVhY2E4YTgwMjYzZDczNTkyNDVhZTJkZjExNjlkMGNlNTE3MDY3NzBiZjk5OGQxOTczZDE1MWM5Mzg).
  * We will use this for communications between the group outside of work hours. 

### tools:
  * [visual studio](https://visualstudio.microsoft.com/downloads/)
     * atleast .net core workload required for week 1
  * [visual studio code](https://code.visualstudio.com/download)
  * [.net core sdk](https://dotnet.microsoft.com/download)
     * lets us compile c# code.
     * included with visual studio workload
     * gives us "dotnet" command
     
### [gitignore](https://github.com/dotnet/core/blob/master/.gitignore) 
  
### Useful Links
* [Git Cheat Sheet](https://www.git-tower.com/blog/git-cheat-sheet)
* [Git Basics](https://youtu.be/0fKg7e37bQE)
* [Git Team Basics](https://youtu.be/oFYyTZwMyAg)

*The most common laptops are Windows PCs. Where MacOS and Linux systems can use package managers, Windows prefers its own GUI wizards.*


* [Hacker Rank](https://www.hackerrank.com/)
  * Good source of practice. Use it often for practice. Of course, if you still have assigned work to do, that work takes precedence.
* [learn about md files](https://guides.github.com/features/mastering-markdown/)
  * it''s always good to read and manage markdowns.
  * Also [markdown cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#headers)
')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (2, N'# Entity Framework Core Steps:

## Package Manager Console (for VS2019 users)
- Run following commands in your Data Layer project in the VS2019 PMC (in your Tools menu option->Nuget Package Manager -> Package Manager Console)
- `Install-Package Microsoft.EntityFrameworkCore.SqlServer`
- `Install-Package Microsoft.EntityFrameworkCore.Tools`
- `Install-Package Microsoft.EntityFrameworkCore.Design`
- Build the project before Scaffolding
- `Get-Help about_EntityFrameworkCore` - See the EF help commands
- `Scaffold-DbContext -connection "Server=<DB Server Name>;Database=<DB Name>;user id=<username>;Password=<password>;" -provider Microsoft.EntityFrameworkCore.SqlServer -outputDir <Output Directory name> -context <context name>` - Its is mandatory to provide connectio string and the provider rest parameters are optional

## .NET core CLI COMMANDS (specially VSCODE users)
- Run following commands in your Data Layer project in the vscode terminal/gitbash/Command prompt)
- `dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
- `dotnet add package Microsoft.EntityFrameworkCore.Design`
- `dotnet add package Microsoft.EntityFrameworkCore.Tool`
- `dotnet ef` - to check EF is installed
- `dotnet ef -h` -See the EF help commands
- `dotnet ef dbcontext scaffold -h` - see if help about EF Db Context Scaffold commands
- `dotnet ef dbcontext scaffold -connection "Server=<server name>;Database=<Db Name>;user id=<username>;Password=<password>;" -provider Microsoft.EntityFrameworkCore.SqlServer -outputDir <Output Directory name> -context <context name>` - to get all Entities mapped from database objects to C# classes.


[Reference](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)


## To move the connection string from generated dbcontext file to JSON configuration file:
1. add package `Microsoft.Extensions.Configuration.Json` to the console app. (turns out the other two packages from before are dependencies that are automatically added)
2. add new item to console app project, type "JSON File", named "appsettings.json", with these contents:

```
{
  "ConnectionStrings": {
    "<name you give to this data source>": "<your connection string>"
  }
}
```
3. right-click on "appsettings.json" in the solution explorer, go to Properties, change "Copy to Output Directory" to "Copy if newer".
4. make a ".gitignore" file in your solution directory, with the contents "appsettings.json".
5. in `Program.Main` of your console app, add the code:

```
var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfigurationRoot configuration = configBuilder.Build();
```
6. to get the options ready to make a dbcontext, use the code (substituting the right stuff):
```
var optionsBuilder = new DbContextOptionsBuilder<YourAppDbContext>();
optionsBuilder.UseSqlServer(configuration.GetConnectionString("<name you gave to the data source>"));
var options = optionsBuilder.Options;
```
7. then, when you need to make a dbcontext (to give you to your repository classes): `new YourAppDbContext(options)`.
')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (3, N'# Super Fun Times')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (4, N'# Super Fun Times
## H2s are fun too')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (6, N'# This is a super cool page
## H2s are important for further categorization of information')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (7, N'# This is a super cool page2
## H2s are important for further categorization of information')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (8, N'# This is a super cool page3
## H2s are important for further categorization of information
### H3s are good too')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (9, N'# This is a test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (10, N'# This is a 2 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (11, N'# This is a 3 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (12, N'# This is a 4 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (13, N'# This is a 5 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (15, N'# This is a 6 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (16, N'# This is a 7 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (17, N'# This is a 8 test page to see why it''s giving me 500')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (18, N'# Page Updated From Put Request
## This is an H2
### This is an H3')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (19, N'# Page Test 2
## This is an H2
### This is an H3')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (20, N'# Test header 1
## Test header 2
### Test header 3')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (21, N'# Test header 1
## Test header 2
### Test header 3')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (22, N'# Page Test 3
## This is an H2
### This is an H3')
INSERT [wiki].[pageMdContent] ([pageId], [MdContent]) VALUES (23, N'# Page Test 4 Modified from PATCH
## This is an H2
### This is an H3')
INSERT [wiki].[wiki] ([Id], [Url], [PageName], [hitCount]) VALUES (1, N'training-code', N'Training Code Wiki', 6)
INSERT [wiki].[wikiHtmlDescription] ([wikiId], [HtmlDescription]) VALUES (1, N'<h1>
<a id="user-content-training-code-wiki" class="anchor" href="#training-code-wiki" aria-hidden="true"><span aria-hidden="true" class="octicon octicon-link"></span></a>Training Code Wiki</h1>
')
INSERT [wiki].[wikiMdDescription] ([wikiId], [MdDescription]) VALUES (1, N'# Training Code Wiki')
/****** Object:  Trigger [wiki].[onChangePageMd]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [wiki].[onChangePageMd]
on [wiki].[pageMdContent]
after update, insert
as
begin
set nocount on;
delete a from wiki.pageHtmlContent a join inserted b on a.pageId = b.pageId;
delete a from wiki.contents a join inserted b on a.pageId = b.pageId;
end
GO
ALTER TABLE [wiki].[pageMdContent] ENABLE TRIGGER [onChangePageMd]
GO
/****** Object:  Trigger [wiki].[onChangeWikiMd]    Script Date: 2020-02-15 17:54:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [wiki].[onChangeWikiMd]
on [wiki].[wikiMdDescription]
after update, insert
as
begin
set nocount on;
delete a from wiki.wikiHtmlDescription a join inserted b on a.wikiId = b.wikiId;
end
GO
ALTER TABLE [wiki].[wikiMdDescription] ENABLE TRIGGER [onChangeWikiMd]
GO
USE [master]
GO
ALTER DATABASE [ViewzDB] SET  READ_WRITE 
GO
