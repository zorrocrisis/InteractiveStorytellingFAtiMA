# **Interactive Storytelling with the FAtiMA Toolkit**
This project, originally an evaluation component for the Artificial Intelligence in Games course (2023/2024), talking place in Instituto Superior Técnico, University of Lisbon, aimed to explore **interactive storytelling in video games** by exploiting the **[FAtiMA-Toolkit](https://github.com/GAIPS/FAtiMA-Toolkit)**, **which harnesses the power of emotional decision making, emotional appraisal and social importance to create and convey believable interactions**.

<p align="center">
  <img src="https://github.com/user-attachments/assets/649151dd-056b-4e73-a26b-3a06ac41a3b7" />
</p>

The following document indicates how to access the source code, utilise the executable application and control the program. It also details the manner through which a story scenario was implemented, the design choices made and some comments considered to be overall relevant to the final project.

## **Source Files and Application**
The project's source files can be downloaded from this repository. To open the program using Unity (v.2021.3.10f1), simply clone the repository and open the project utilising Unity Hub.

To test the application, only the files contained in the "Build" folder are necessary. Once those files are downloaded, simply start the executable (see the controls below).

## **Application's Controls**

Main Menu:
- **LMB** interacts with the main menu's buttons, selecting the decision making algorithm and exiting the application.

In Simulation:
- **Esc** exits to the main menu.


## **Development - Test Scenario**
Before building a main story, a simple test scenario was implemented in FAtiMA, considering the prompt below. The resulting *.json* files are called *introductory_scenario*, which holds information regarding the scenario itself, and *introductory_scenario_cogrules*, which takes into account necessary cognitive rules. These are the two main file types when building a scenario in FAtiMA, and can be encountered [here](https://github.com/zorrocrisis/InteractiveStorytelling_FAtiMA/tree/main/Assets/StreamingAssets/SingleCharacter).

**Story prompt:** *“Peter was hungry so he went to a restaurant. Once there he ordered a hotdog. The waiter told him they only served hamburgers. Peter told the waiter that was okay as well. After a while the waiter brought Peter his food. The hamburger was burnt to a crisp. Peter complained to the waiter. The waiter told Peter they had no more hamburgers. Peter immediately left the restaurant without paying.”*

After identifying **two different emotional agents** (Peter and Waiter), specific **Beliefs were created to satisfy the needs and fiction of the story** – *Has(Hamburger)*, *Has(Money)* and *Is(Hungry)*. Through FAtiMA's **Emotional Decision Making** tab, **Action Rules were implemented to account for the different speaking Styles (Polite and Rude)** and the dialogue’s evolution with the Cook action, which in turn was created in the World Model tab. Moreover, **Appraisal Rules allowed the agents’ dialogue to directly influence their mood** (although in a basic way, in this case only using Desirability) and, finally, the dialogue states and corresponding utterances were developed in the Dialogue Editor. The resulting dialogue graph is shown here:

<p align="center">
  <img src="https://github.com/user-attachments/assets/63e0ace0-ed35-4a70-959a-33610e422dae" />
</p>

The whole scenario is built around 12 dialogue states, being important to mention the Cook action allows a “jump” from the Waiting sate to the Serving state (just like in a real restaurant).

Another additional note: utilising the FAtiMA-Toolkit, the user can both witness the interaction play out between the two emotional agents or take the role of one of the characters.
 
From this short scenario, one major comment should be brought up: **the action rules consider the agents’ moods when verifying whether the speaking actions are possible...** As an example, an agent will only speak politely (good desirability) if their mood is positive, which, of course, does make some sense. However, **this implementation makes the back-and-forth dialogue (and consequently the entire interaction) quite one-dimensional and easily “snowballed”**: if the first agent to speak decides to do so in a rude manner, then the whole conversation is likely to have the same course, with the moods of both agents gradually decreasing with no end in sight. Having said this, another approach was utilised and is explained in detail in the final scenario.

## **Development - Final Scenario: "Bartender Trouble"**
The second and final implemented scenario did not have a story prompt to follow and, unlike the previous one, was simultaneously developed in Unity (v.2021.3.10f1), taking advantage of **character models with adaptable facial expressions to further increase the believability of the interactions portrayed**. The resulting *.json* files are called *bartender_trouble*, which holds information regarding the scenario itself, and *bartender_trouble_cogrules*, which takes into account necessary cognitive rules. These files can be opened utilising FAtiMA and can be found [here](https://github.com/zorrocrisis/InteractiveStorytelling_FAtiMA/tree/main/Assets/StreamingAssets/SingleCharacter).

After some brainstorming, it was decided the final scenario would be structured around a bar called *Missed Chance*, in which the Bartender interacts with two other role-play characters: the Drunkard (aka, Earl) and the Customer. The scenario begins with the Customer entering the bar, ordering a drink and striking a conversation with the Bartender, which unexpectedly unfolds into a weird and/or wholesome sequence of reflections and food for thought. We could state the overall theme of the scenario is empathy – the player (the Bartender) succeeds by helping the Customer.

![imagem](https://github.com/user-attachments/assets/ce699757-552f-40fb-8173-2afb12666ad6)

Considering the previous information, four **different speaking styles were implemented**: Heartless, Empathic, Sad and Nostalgic. **The first two are mainly employed to convey emotions which result from direct interactions with other agents**. For example, if the Bartender chooses to speak with empathy towards the Customer, the latter will feel a sense of Desirability and Praiseworthiness for the former, in turn originating emotions liked Gratitude, Joy and Admiration according to the **OCC Model** (unfortunately, some of these are not expressed through the character models used in Unity due to technical limitations). **The two other speaking styles**, Sad and Nostalgic, are linked to emotions that agents feel during dialogue but **are not exactly a direct result of interactions with other agents**. This contrasts with the approach previously mentioned and used in the first scenario. As an example, let us consider the following section of the dialogue tree.

<p align="center">
  <img src="https://github.com/user-attachments/assets/f2457930-ab4f-44ab-9497-9762f5872138" />
</p>

<p align="center">
  <img src="https://github.com/user-attachments/assets/3d233149-dea2-4bf6-8965-2d54f1f4280a" />
</p>

When the Bartender enters the *Clarity1Response* state, the agent will have Empathic and Heartless speaking options available. However, instead of an Empathic response only leading to another certain positive reply (like in the first scenario), the agents enter the *Conversation1* state, which still possesses a likelihood of performing a Sad speak action.

This **mood-independent approach** to selecting dialogue options also came as a solution to a previous problem – an agent (in this case, the Customer) entering the scenario with a negative mood. With the approach of the first scenario, we would have to implement character-specific rules to define the “mood baseline” of that character and to determine their possible actions (this can easily become unmanageable with multiple characters and styles). To sum up, **mood-independent sequences seems to ultimately grant more unpredictability, while still maintaining a reasonable dialogue**. For example, at the beginning of the scenario the Bartender can tell a joke and not know if it is going to be well received by the Customer… In fact, no one knows! `**The downside of this approach is undoubtedly the increase in the amount of planning needed**.

Another relevant authoring tool component was also implemented in this scenario is the **Social Importance**. In the case of the present scenario, **this tool unlocks certain sections of the dialogue**. For instance, considering the Bartender’s goal of helping the Customer: by increasing the latter’s mood, the former’s social importance will grow, thus leading to a section of the dialogue where we can know more about the Customer’s story (*DeepConversationResponse* state). If the Social Importance is too low, however, the Bartender fails this attempt at gaining more insights (*DeepConversationFailed* state). This tool is once again utilised as a criterion for unlocking the *BestEnding*. Speaking of endings…

In total, there are **three main endings**, which highly depend on whether the Bartender manages to help the Customer. As previously mentioned, the *BestEnding* is “hidden” behind a dialogue option only unlockable through a high Social Importance perspective of the Bartender. The *NeutralEnding* and *BadEnding* can both be achieved during the last stages of the dialogue without any restriction. Either way, once the scenario ends, there is a Unity UI indication of which ending was reached, alongside a final message related to empathy.

<p align="center">
  <img src="https://github.com/user-attachments/assets/1edddb7c-1819-417d-8e3b-b3081aec5504" />
</p>

So far, the scenario’s third agent, the Drunkard, hasn’t really been mentioned… This character serves almost as a comic relief, interrupting the dialogue between the two other characters. These **interruptions are based in actions that command changes in dialogue states and in Meanings attributed to speak actions**, through which the conversation is expected to resume (speak actions with a *ResumeConversation* Meaning). The combination of these factors allows **smooth transitions between the Bartender-Customer dialogues and the Bartender-Drunkard dialogues**.

<p align="center">
  <img src="https://github.com/user-attachments/assets/1f884b02-68ad-4e6b-b0b3-94c6074be02c" />
</p>

Finally, it is worth leaving some comments related to **Unity’s implementation of the scenario**:

- The connection between the FAtiMA-Toolkit and the Unity initially brought some minor problems: although in the authoring tool’s simulator the dialogue actions of the same style (and same dialogue state) were shuffled and randomly picked by the agents, **Unity seemed to only select a single dialogue option of a given style (and of a given state) each run**. This issue led to **limiting dialogue sequences and bland interactions**. To overcome said issue, some **Action Rules were created to separate the dialogue options with the same styles into different groups with an equal priority rating** (the Meaning parameter of the speak actions was used for this effect – Good2, Sad2, Happy1, ChitChat4, Inspiration2 are just some examples). Consequently, Unity is now forced to pick between actions with the same priority, thus randomizing the dialogue options;
  
<p align="center">
  <img src="https://github.com/user-attachments/assets/7346a26a-cebf-40b0-a92a-f0d931b017e7" />
</p>

- In this scenario, **non-speak actions** (like the Cook action in the previous scenario) **were not implemented, due to time constraints**. In their place, there are dialogue options with asterisks on both sides representing an action that could, in theory, be created inside Unity in a more interactive way (linked to resource management, for example). Despite this, a simple showcase of how these actions can provide a more engaging storytelling experience can be witnessed at the end of the *FinalOrder* state, where a cup is placed near the Customer and a sound effect of liquid pouring is heard (done with the aid of a flag - *serveDrink* - which becomes true after reaching the aforementioned state).

- Regarding, once again, the improvement of the scenario’s believability in Unity, multiple efforts were made to this end. Firstly, **a simple bar model was imported and altered to represent the scenario’s stage** (the *Missed Chance* bar). Secondly, **directional lights** were implemented to simulate the overall dim lighting of a run-down bar. Finally, the scene’s **skybox** was changed so you could see the night sky through the bar’s windows;

## **Final Remarks**
As a closing remark, one could say the **FAtiMA-Toolkit undoubtedly facilitates the creation of interactive moments and emotionally intelligent agents, despite there being a multitude of different approaches and interpretations as to how these agents can/should be implemented**.

As also aforementioned, the project allowed the exploration of two distinct ways of managing emotional dialogues - **mood-dependent** and **mood-independent** -, both proving to have considerable advantages and disadvantages. We can state the dimension of the project at hand can have a heavy influence on what approach to utilise - where **the mood-independent approach requires more attention to the overall structure of the story**, **the mood-dependent relies on a simple “rule-of-thumb” that is more appropriate for less in-depth and quick storytelling**.

Lastly, it’s clear to see how a fully explored and long-term conjunction between a Unity scene and the FAtiMA’s toolkit can lead to an immersive experience like no other, bridging the gap between storytelling and gameplay systems.

*Did you waste the day, or lose it, was it well or sorely spent? / Did you leave a trail of kindness or a scar of discontent? / As you close your eyes in slumber do you think that God would say, / You have earned one more tomorrow by the work you did today?* - Edgar Guest, Have You Earned Your Tomorrow
  
## **Authors and Acknowledgements**

This project was developed by **[Miguel Belbute (zorrocrisis)](https://github.com/zorrocrisis)**.

The initial code was supplied by **[Prof. Manuel Guimarães](https://fenix.tecnico.ulisboa.pt/homepage/ist172992)**.

