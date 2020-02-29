Contributing
================================================================================

First, thank you so much for your interest in sharing your time, knowledge,
and ignorance for this project.

The following are some guidelines to help make Solution Processing consistent in
the Github repository, Git history, and codebase.

> Note: You must [login](https://github.com/login) to GitHub to contribute

Table of Contents
--------------------------------------------------------------------------------

* [Creating an Issue](#creating-an-issue)
  * [Issue Convention](#issue-convention)
* [Making Changes](#making-changes)
  * [Branch Naming](#branch-naming)
  * [Commits](#commits)
  * [Pull Requests](#pull-requests)
* [Code Conventions](#code-conventions)

Creating an Issue
--------------------------------------------------------------------------------

* The issue tracker is reserved for bugs, perceived bugs, questions, and
  feature requests
* Ensure the [issue] doesn't **already exist**
* Use the appropriate issue template

  ![Issue template selection](/.github/images/issue_template_selection.png)

* Do your best to give a descriptive short subject title
* Explain what the issue is and include any information that can be linked

Contributing Changes
--------------------------------------------------------------------------------

This repository uses the [Github flow](guides.github.com/introduction/flow/):

* Quickly ensure a [pull request] or [issue] doesn't already exist regarding
  the change you want to make
  * For large changes, create a related issue reviewing the proposed change to
    prevent wasted work
* [Fork] this repo to make a personal copy on your account
* [Clone] your personal fork on your machine
* Select a branch to branch from
  * `master` if you're fixing a bug on master
  * `develop` if your changing anything else
* [Create a feature branch] with a meaningful name
* Make changes on the created branch
* [Commit] your changes to your local repository under the branch just created
  * Be thoughtful of the [commit guidelines](#commit-guidelines)
* [Create a pull request] once desired changes have been committed
  * If you're looking for active feedback, it can be helpful to create a
    [draft pull request] before all changes have been made
  * Be thoughtful of the [pull request guidelines](#pull-request-guidelines)
* Wait for feedback and merge completion

### Commit Guidelines

* Ensure changes are **relevant** to the changes you **intend** on making
* Make commits in **logical** units of relevance and **limited** in number
* Follow [Chris Beams's seven rules for commit messages]
  1. Separate subject from body with a blank line
  2. Limit the subject line to 50 characters
  3. Capitalize the subject line
  4. Do not end the subject line with a period
  5. Use the [imperative mood] in the subject line
  6. Wrap the body at 72 characters
  7. Use the body to explain what and why not how

### Pull Request Guidelines

* Do your best to give a descriptive short subject title
* Use the appropriate pull request template

Code Conventions
--------------------------------------------------------------------------------

In general, this repository uses [Microsoft's C# Coding Conventions]. However,
exceptions to Microsoft's conventions and more detail about this projects
conventions can be seen in the [Code Conventions](CODE_CONVENTIONS.md)
document which has examples of how to format code for this project.

[Microsoft's C# Coding Conventions]: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions
[imperative mood]: https://en.wikipedia.org/wiki/Imperative_mood
[Chris Beams's seven rules for commit messages]: (https://chris.beams.io/posts/git-commit/#seven-rules)
[Fork]: (https://help.github.com/articles/fork-a-repo/)
[Clone]: (https://help.github.com/en/github/creating-cloning-and-archiving-repositories/cloning-a-repository)
[Commit]: (https://services.github.com/on-demand/github-cli/add-commits-git)
[Create a pull request]: (https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request)
[pull request]: (https://github.com/mojur/solution-processing/pulls)
[issue]: (https://github.com/mojur/solution-processing/issues)
[Create a feature branch]: (https://docs.github.com/en/desktop/contributing-and-collaborating-using-github-desktop/managing-branches)
[draft pull request]: (https://github.blog/2019-02-14-introducing-draft-pull-requests/)
[How to write the perfect pull request]: (https://github.blog/2015-01-21-how-to-write-the-perfect-pull-request/)
