# MiX Game

## About The Project

This project originated as an anniversary present for my girlfriend, and using the opportunity to showcase my game development skills. Over the course of two weeks, I dedicated myself to creating a game that balanced simplicity with quality code architecture.

I also need to mention i was deeply and clearly inspired by the charming aesthetic and fluid movement of Overcooked 2. It was a goal to try and recreate those elements in Unity by myself.

After the first personal version was finished, I took some more time to refine and optimize the game, with the goal of finally including it in my professional portfolio.

<div style="text-align: center;">
    <img src="https://github.com/XavierMerino01/MiX-TriviaGame/assets/71768212/d1fb81ff-da98-4486-8538-f9ba69012740" alt="Captura5" width="450">
</div>

## Gameplay
The game starts giving players the option to choose between three characters in the Main Menu: a duck, a bear, and a dog, created by KayLousberg and available for use on itch.io. 

<div style="text-align: center;">
    <img src="https://github.com/XavierMerino01/MiX-TriviaGame/assets/71768212/7ddc8a3a-677a-4218-9f69-52d313265490" alt="Captura" width="450">
</div>

The objective of the game is to collect as many blue diamonds as possible, unlocking rewards along the way. To acquire diamonds, players must engage in a series of challenges, including trivia-style games and a "complete the room" puzzle. 

<div style="display: flex;">
    <img src="https://github.com/XavierMerino01/MiX-TriviaGame/assets/71768212/58995963-79c6-4511-931a-a489c2dc2d87" alt="Captura2" width="350">
    <img src="https://github.com/XavierMerino01/MiX-TriviaGame/assets/71768212/c960ab3d-314f-4f3f-a151-35851103fd7f" alt="Captura3" width="350">
</div>


## Code Overview 
The development of the code was key, so I focused on creating a cohesive and efficient codebase to support the implementation of various gameplay features. Below is an overview of the key aspects of the code architecture and design principles utilized throughout the project.

#### Modular Architecture
I implemented a modular architecture with the GameManager serving as the central control hub, facilitating communication between different game components, enabling scalable development and trying to avoid "spaghetti code".

#### Object-Oriented Design
Focused in object-oriented programming (OOP) principles by leveraging inheritance and polymorphism in scripts (such as FurnitureObject and GameButton both children of Interactable script) resulting in reusable and extensible codebase.

#### Player Interaction
Managed player input and character control through the PlayerScript and InputManager, learning to handle Unity's input system and ensuring responsive player interaction.

#### Puzzle Mechanics
Implemented simple but engaging puzzle mechanics, including the furniture completion puzzle and the trivia challenges.

#### State Management
Utilized state management techniques, such as coroutines and enum-based state machines, to orchestrate game events and manage the progression of gameplay elements with precision and efficiency.

#### Audio and Effects
Created an audio and VFX system to play the diferent necessary cues and to also play the musical pieces when needed for the puzzles. 


