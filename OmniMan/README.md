# OmniMan - Unity Project

A Unity project featuring OmniMan character assets and gameplay.

## Prerequisites

- **Unity Version**: Open `ProjectSettings/ProjectVersion.txt` for the required Unity version
- **Git LFS**: This project uses Git Large File Storage for binary assets. Install it before cloning:
  - Windows: `winget install GitHub.GitLFS` or download from [git-lfs.com](https://git-lfs.com)
  - macOS: `brew install git-lfs`
  - Linux: `sudo apt install git-lfs`

## Setup

1. **Clone the repository** (Git LFS must be installed first):
   ```bash
   git clone <repository-url>
   cd OmniMan
   ```

2. **Install Git LFS hooks** (if not done automatically):
   ```bash
   git lfs install
   ```

3. **Pull LFS files** (if assets appear as placeholder files):
   ```bash
   git lfs pull
   ```

4. **Open in Unity**:
   - Open Unity Hub
   - Click "Add" and select this project folder
   - Unity will import assets on first open (may take several minutes)

## Project Structure

```
OmniMan/
├── Assets/           # All game assets, scripts, and scenes
│   ├── Scenes/       # Unity scene files
│   ├── Scripts/      # C# scripts
│   ├── Animation/    # Animator controllers and animation clips
│   └── ...
├── Packages/         # Unity package manifest
├── ProjectSettings/  # Project configuration
└── .gitattributes    # Git LFS configuration
```

## Controls

(Add your game controls here)

## Credits

- OmniMan 3D model
- [Add other asset credits]

## License

(Add your license here)
