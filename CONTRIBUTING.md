# Contributing to LightCore

The following is a set of guidelines for contributing to LightCore. It is still work in progress.

#### Table Of Contents

* [How to Write a Good Commit Message](#how-to-write-a-good-commit-message)

## How to Write a Good Commit Message

Important Rules
* Seperate subject from body with a blank line
* Capitalize the subject line
* Do not end the subject line with a period
* Use the imperative mood in the subject line, like: "Add xyz", "Fix xyz". Not "Added xyz".
* The first Line (subject) should be at max. 50 characters
* The lines after the blank separation line (body) should be at max. 72 characters
* Use the body to explain the what, why and how

Example for a good commit message:
```
Add new test for LightCoreConfiguration

This adds a new test for the LightCoreConfiguration,
as the configuration was extended with new properties.
```

Example for a bad commit message:
```
Added new test for LightCoreConfiguration because the configuration was extended with new properties.
```

See also: https://chris.beams.io/posts/git-commit/
