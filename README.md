# ai_director
## Intro
ai_director is the first ai show engine that supports stage directions! The possbilities are endless. From creating full AI episodes (that aren't just dialogue), to extra fun and wacky generations.

## Installation
To install, create a 3D unity project. Install tmpro, cinemachine, and [OpenAI Unity](https://github.com/srcnalt/OpenAI-Unity).

Then drag all of the files from the repo, into your assets folder. That's it.

**YOU NEED A API KEY, FOLLOW OPENAI-UNITY'S INSTRUCTIONS ON SETTING UP A API KEY JSON**

## How it works
### Manager
ai_director is mainly powered by one script. The **manager**. The manager handles ai generation, along with subtitle managment. To use it, make a empty gameobject, attach the manager.

Then fill out the parameters in the inspector.

### Character
The character script handles character movement. In the original ai_engine I made, it also handled looking, but since this is a prototype, we don't care about that for now.

### Stage Direction
The base class to derive from when making a stage direction.
