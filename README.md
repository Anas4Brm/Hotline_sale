# TopDown AI Game

This is a top-down 2D action game project built using Unity. It includes enemy AI, player aura detection, and various game mechanics inspired by fast-paced arcade-style gameplay.

## Project Details

- **Unity Version**: Unity 6 (6000.0.37f1)
- **Project Type**: 2D Top-Down Shooter
- **Key Features**:
  - Enemy AI with detection and chasing behavior
  - Player aura that changes color when enemies are near
  - Customizable camera and scene structure
  - Modular scripts and asset organization

## Getting Started

1. Open the project with Unity 6 (6000.0.37f1) or later.
2. Load the `Level1` scene.
3. Press Play to test the gameplay.

## Scripts

- `AuraReaction.cs`: Handles visual feedback based on nearby enemies.
- `PlayerBehavior.cs`: Manages player input and movement.
- `GameManager.cs`: Centralizes game control and state.

## Layers

Make sure the **NPC_Enemy** objects are set to the `Enemy` layer to properly interact with the player aura system.

## Notes

- Ensure you have all required materials and shaders assigned.
- Use the `TopDown_AI` folder structure for reference when adding new content.
