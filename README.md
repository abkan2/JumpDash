# JumpDash

*An endless‑runner built with **Unity 2022.3.44f1***

<div align="center">
  <img src="Assets/Art/JumpdashTitle.png" alt="JumpDash title screen" width="600"/>
</div>

---

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Gameplay & Controls](#gameplay--controls)
4. [Project Structure](#project-structure)
5. [Requirements](#requirements)
6. [Getting Started](#getting-started)
7. [Building](#building)
8. [Roadmap](#roadmap)
9. [Contributing](#contributing)
10. [License](#license)

---

## Overview

**JumpDash** is a fast‑paced 3D endless‑runner set in a neon‑soaked cityscape. Sprint, jump and dash your way through procedurally‑spawned obstacles while hoarding gems to climb the global leaderboard.

The project was created as a learning exercise in gameplay programming, UI/UX, and production‑quality Unity workflows. Feel free to fork, dissect, and remix!

---

## Features

| Category         | Details                                                                             |
| ---------------- | ----------------------------------------------------------------------------------- |
| **Core Loop**    | Infinite forward motion with increasing speed every 5 gems collected.               |
| **Obstacles**    | Dynamic drones, traffic barricades, and collapsing road tiles generated at runtime. |
| **Power‑Ups**    | Shield pick‑up grants 6 s invulnerability.                                          |
| **Spawn System** | `SpawnManager` cycles between normal, fast, and slow intervals based on game state. |
| **Polished UI**  | Animated title, countdown (3‑2‑1‑GO!), responsive buttons, how‑to‑play overlay.     |
| **Audio**        | Layered SFX + adaptive music that ramps up with speed.                              |
|       |                                 |

---

## Gameplay & Controls

| Action            | Keyboard  | Gamepad      |
| ----------------- | --------- | ------------ |
| Move left / right | ← / →     | Left stick X |
| Jump              | **Space** | A / ✕        |
| Pause             | **Esc**   | Start        |

**Objective:**

* Collect as many gems as possible.
* Every 5 gems increases speed & difficulty.
* Hitting an obstacle ends the run (unless shield is active).

---

## Project Structure

```text
JumpDash/
├─ Assets/
│  ├─ Art/
│  │  ├─ Environment/
│  │  └─ UI/
│  ├─ Prefabs/
│  ├─ Scenes/
│  │  ├─ MainMenu.unity
│  │  └─ Game.unity
│  ├─ Scripts/
│  │  ├─ Gameplay/
│  │  │  ├─ PlayerController.cs
│  │  │  └─ SpawnManager.cs
│  │  └─ UI/
│  └─ Audio/
├─ Packages/
└─ ProjectSettings/
```

---

## Requirements

* **Unity Hub** with **Unity 2022.3.44f1 LTS** (any platform modules you intend to build for).
* GPU capable of OpenGL 3.3 / Metal or higher.
* Tested on Windows 10/11 & macOS 12+.

---

## Getting Started

1. **Clone** the repository:

   ```bash
   git clone https://github.com/your‑username/JumpDash.git
   ```
2. **Open** the folder in **Unity Hub** → *Add* → select the project root.
3. Ensure the **Editor Version** column shows **2022.3.44f1** (click the dropdown to switch if needed).
4. Press **Play** in the Unity Editor to try it out.

### Optional: Seed sample data

Inside *Game* scene, toggle the **DebugSpawner** object to pre‑spawn obstacles & gems for QA.

---

## Building

> 🛠️ **Note:** All build presets live under `Assets/Editor/BuildSettings`.

### Desktop (Windows/macOS)

1. File ▸ Build Settings…
2. Select **PC, Mac & Linux Standalone**.
3. Click **Add Open Scenes** (ensure `Scenes/Game.unity` is listed first).
4. Choose target platform & architecture.
5. Hit **Build And Run**.

### WebGL

1. Install WebGL module via Unity Hub if missing.
2. Switch platform to **WebGL** in Build Settings.
3. Build → output to `Builds/WebGL/`.
4. Serve locally with any static‐file server or deploy to Itch.io.

---

## Roadmap

* [ ] Power‑up variety (double‑jump, magnet).
* [ ] Online leaderboard via Firebase.
* [ ] Mobile touch controls & haptics.
* [ ] Endless‑runner level chunks authored in ProBuilder.
* [ ] Post‑process neon glow / HDR bloom.

Got a feature idea? Open an issue or start a discussion!

---

## Contributing

1. Fork the repo & create a branch: `git checkout -b feature/my‑feature`.
2. Commit your changes: `git commit -m "Add cool feature"`.
3. Push to the branch: `git push origin feature/my‑feature`.
4. Open a Pull Request and describe what you’ve done.

> **Style guide:** Follow the C# conventions in `.editorconfig` and keep assets under 10 MB when possible.

---

## License

**JumpDash** is released under the MIT License. See [`LICENSE`](LICENSE) for details.

Happy running! 🏃‍♂️💨
