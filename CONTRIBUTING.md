Contribution to xunit.di
=====================
You can contribute to xunit.di with issues and PRs. Simply filing issues for problems you encounter is a great way to contribute. Contributing implementations is greatly appreciated.
## DOs and DON'Ts

Please do:

* **DO** include tests when adding new features. When fixing bugs, start with
  adding a test that highlights how the current behavior is broken.
* **DO** keep the discussions focused. When a new or related topic comes up
  it's often better to create new issue than to side track the discussion.

Please do not:

* **DON'T** surprise us with big pull requests. Instead, file an issue and start
  a discussion so we can agree on a direction before you invest a large amount
  of time.
* **DON'T** commit code that you didn't write. If you find code that you think is a good fit to add to xunit.di, file an issue and start a discussion before proceeding.

## Suggested Workflow

We use and recommend the following workflow:

1. Create an issue for your work.
    - You can skip this step for trivial changes.
    - Reuse an existing issue on the topic, if there is one.
    - Get agreement from the team and the community that your proposed change is a good one.
    - Clearly state that you are going to take on implementing it, if that's the case. You can request that the issue be assigned to you. Note: The issue filer and the implementer don't have to be the same person.
2. Create a personal fork of the repository on GitHub (if you don't already have one).
3. In your fork, create a branch off of main (`git checkout -b mybranch`).
    - Name the branch so that it clearly communicates your intentions, such as issue-123.
    - Branches are useful since they isolate your changes from incoming changes from upstream. They also enable you to create multiple PRs from the same fork.
4. Make and commit your changes to your branch.
5. Add new tests corresponding to your change, if applicable.
6. Build the repository with your changes.
7. Create a pull request (PR) against the xunit.di repository's **main** branch.
    - State in the description what issue or improvement your change is addressing.
8. Wait for feedback or approval of your changes
9. Your PR will be merged once approved.
    - The next official build will automatically include your change.
    - You can delete the branch you used for making the change.