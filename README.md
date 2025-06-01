## Team Name: 

I Tried Escaping My Yandere Girlfriend, But She Still Found Me!?

---

## Proposed Level of Achievement: 

Apollo

---

## Motivation 

This project is designed as a portfolio piece to appeal to Japanese companies, showcasing a unique blend of pixel art, horror storytelling, and game development skills. Japanese horror games like The Witch’s House and Ao Oni have gained a strong following due to their unsettling atmosphere and impactful death sequences. These games have demonstrated that effective horror storytelling does not require high-end graphics but strong artistic direction and well-crafted concepts.

By developing a visually striking horror experience, we aim to demonstrate our ability to create a unique, high-quality indie horror pixel-art game that captures the essence of Japanese horror. We are committed to delivering polished pixel art, cutscenes with high tension and suspense and eerie gameplay to attract players. 

Through this project, we hope to position ourselves as developers who can contribute meaningfully to the horror genre as well as leveraging our artistic and programming expertise to showcase our ability to create a unique horror experience.

---

## Aim 

We hope to develop a pixel-art horror game that revolves around unexpected and dynamic death sequences. Players will make quick decisions to escape a dangerous, obsessive Yandere girlfriend, only to find that every action leads to a different horror experience. The game will focus on atmospheric tension, decision-drive interactions, and immersive horror storytelling through gameplay. 
Our main objects include:
- Creating a tense, immersive horror experience through a combination of hand-drawn pixel art, psychological storytelling, and eerie sound design.
- Developing a dynamic, choice-driven escape system where each decision branches into different death sequences, making every playthrough unique.
- Testing our creativity and technical skills by ensuring that every element, from gameplay mechanics to pixel art animations, aligns with the eerie, immersive atmosphere we want to achieve.
Our goal is to fully utilize our artistic and programming abilities to challenge ourselves and deliver a horror experience that stands out by combining game design, pixel animation and psychological horror storytelling to craft something unique.

---

## User Stories

- As a horror game player who enjoys psychological horror, I want to be able to experience multiple death animations based on my choices, so that I feel truly immersed in the game’s tension and fear.
- As a casual player who enjoys suspenseful storytelling, I want to be able to explore different ways to escape and trigger unique endings, so that I feel encouraged to replay the game multiple times.
- As an indie game enthusiast who appreciates pixel art, I want to be able to see detailed, hand-drawn death CGs and animations, so that I can fully experience the artistic horror style of the game.
- As a completionist gamer, I want to discover hidden interactions and secret endings, so that I feel rewarded for thoroughly exploring different escape attempts.

---

## Setting

The story begins with the main character being gently guided into a room by a girl who introduces herself as his girlfriend. The atmosphere is quiet, almost comforting, a soft conversation unfolds between them as they talk about their relationship, their feelings, and what they are to each other.

But something feels off. Her tone is sweet, but her words grow colder. What starts as playful banter slowly turns into unsettling questions, and the air becomes heavier with every line. Before he can make sense of it, her emotions spiral and the protagonist finds himself caught in something far more dangerous than he imagined as she is revealed to be a Yandere.

---

## Features

### Dynamic Yandere Chasing Mechanic

[Proposed]

The core mechanic of the game revolves around a tense, time-sensitive escape sequence triggered by the Yandere girlfriend’s emotional breakdown. The scene begins innocently enough: the player is led from the living room into the bedroom by their girlfriend. However, during a conversation in her room, she got upset and have an emotional breakdown, signifying a potential sign of betrayal.

Consumed by the fear of being abandoned, she makes a chilling decision: if she cannot trust him to stay, she will make sure he can’t leave.

At the moment her behavior becomes erratic, a five-second countdown begins — giving the player just enough time to escape the bedroom. If the player successfully reaches the living room, a second timer of fifteen seconds is triggered. During this brief window, the player must choose how to respond to the situation: whether to hide in the closet, arm themselves with a kitchen knife, or attempt another desperate action, etc which will be the game's selling point.

If no decision is made before the timer expires, the Yandere girlfriend will emerge from the bedroom and begin chasing the player. From that moment on, no further interactions are possible — the only option is to run. The chase sequence is designed to evoke panic and urgency, with the player desperately attempting to outrun her through the house. If she catches up, a jumpscare death scene will play at the end.

However, even if the player makes a decision within the 15-second window, the result will still result in death. A unique cutscene will play depending on the chosen action, but each one inevitably ends in the player’s death.

[Current]
For Milestone 1, the chasing mechanic is implemented as a scripted sequence that transitions smoothly from cutscene to gameplay. The game begins with a fixed cutscene where the player is led into the bedroom by the Yandere girlfriend for a chat. As the conversation unfolds, her emotions slowly unravel, culminating in a breakdown. The scene ends with the protagonist fleeing into the living room in fear, this entire sequence is non-interactive and presented as a cutscene.

Once the player regains control in the living room, the chase begins. The Yandere girlfriend starts pursuing the player immediately, and the player must escape by navigating around the environment but it will always result in death.

Although the full decision system and branching cutscenes are not yet implemented, this version effectively sets up the intended tension and urgency of the chase.

---

### Multiple Death Animations & Triggers

[Proposed]

To heighten the psychological horror experience, the game will feature multiple unique death sequences based on the player’s actions. Whether the player chooses to hide in a closet, crawl under a sofa, or attempt another desperate action, each choice will result in a distinct death animation that reflects the Yandere's twisted affection and escalating instability.

These deaths will be presented as cinematic pixel-art sequences enhanced by screen shake effects, chilling sound design, and subtle visual cues to build tension. When the Yandere physically touches the protagonist, a fully illustrated jumpscare cutscene will play, sharply contrasting the pixel art style with a digital animated format to emphasize the emotional shock and finality of the moment.

Even if the player manages to escape the bedroom after the initial 5-second timer, the outcome remains fatal. Each path will lead to its own custom cutscene.

After each sequence, a game over screen will appear, offering the player a choice to retry or return to main menu.

[Current]

As of Milestone 1, a custom jumpscare sequence has been implemented to represent the player's death when caught by the Yandere. This animation consists of six hand-drawn frames created in Clip Studio Paint and plays immediately after contact is made, accompanied by a game over screen. The art style of the jumpscare contrasts with the pixel-art gameplay, providing a sudden visual and emotional shift to amplify the horror impact.

Other death variations based on player decisions — such as hiding in specific locations or post-timer escape outcomes have not yet been implemented. Additionally, cinematic pixel-art death sequences are currently in the planning stage and will be developed in future milestones to expand the variety and immersion of death outcomes.

---

### Interactive Environment & Decision Based Gameplay

[Proposed]

The game aims to incorporate interactive environments that allow the player to engage with various objects such as hiding spots and items. These interactions form the foundation of a branching decision system, where each choice leads to a unique outcome which will lead to a very horrifying and disturbing death.

When the player reaches an interactive object, they will be presented with context-sensitive options such as “Hide” or “Nevermind.” During this decision-making phase, the chase timer will temporarily pause to give the player space to react under pressure. Once a selection is made, the game will immediately transition into a cutscene based on that choice, emphasizing the psychological horror element and reinforcing the theme that no matter what the player does… they cannot escape.

These interactions will create a layer of tension and player agency, encouraging multiple playthroughs to uncover all the possible outcomes hidden within the environment.

[Current]

As of Milestone 1, the interactive environment and decision-based gameplay elements have not yet been implemented. While the current prototype includes a functional chase sequence, the player cannot interact with objects or make choices during gameplay. The framework for detecting interactable objects and presenting timed decision prompts is still under development.

These features are slated for future milestones, where they will form a core part of the gameplay experience by introducing branching outcomes, and unique cutscenes based on the player's actions.

---

### Horror Sound Design & Screen Effects

[Proposed]

To deepen the immersive horror experience, the game will feature a dynamic sound system that responds to the Yandere’s emotional state and the countdown timer. As her behavior becomes more erratic and the player’s time runs out, the background music will gradually intensify, amplifying the sense of urgency and dread.

In addition to music, sound effects and screen shake will be used in pixel art death animations to simulate a sense of horror. These combined audio-visual elements aim to create a visceral horror atmosphere that pulls the player deeper into the experience.

[Current]

As of Milestone 1, the dynamic horror sound system and screen effects have not yet been implemented. The current prototype does not include background music or audio cues tied to the Yandere’s behavior or the chase timer.

These enhancements are scheduled for future development to heighten the tension and emotional intensity of key moments in the game. Once added, they will serve as crucial tools to immerse the player and reinforce the psychological horror tone of the experience.

---

### Option to Change to Japanese Language

[Proposed]

Future milestones for extended features is to increase accessibility and broaden appeal to Japanese-speaking audiences, the game will include an option to switch the interface and dialogue text to Japanese. This feature not only enhances readability for native players but also positions the game as a more compelling portfolio piece when applying to Japanese companies. Localization efforts will ensure that tone, nuance, and cultural context are preserved across both languages.

---

### Refined Pixel Art and Animation

[Proposed]

Future milestones for extended features will also focus on improving the overall visual polish of the game. This includes increasing the frame count in animations to achieve smoother, more fluid motion during both gameplay and cutscenes. Character pixel art will also be enhanced for greater emotional expression and visual clarity. In addition, dialogue scenes will feature improved character portraits and supporting illustrations in a digital art style to strengthen the narrative presentation and emotional impact of each interaction.

---

## Coding Standard

- Class and Method names should always be in Pascal Case 
- Method argument and Local variables should always be in Camel Case  
- Avoid the use of underscore while naming identifiers  
- Avoid the use of System data types and prefer using the Predefined data types.  
- For better code indentation and readability always align the curly braces vertically.  