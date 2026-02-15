# HattrickAnalyzer

A simple desktop tool to import Hattrick player data from CSV, analyze and compare players (especially new or young ones), estimate approximate TSI values, and export the results to Excel.

## Features

- Load player data from a CSV file  
- Analyze and compare new or young players  
- Estimate **current and projected TSI values** (approximation based on skills and age)  
- Recommend optimal training categories  
- Add player rows manually in the UI  
- Export analyzed results into an Excel spreadsheet  

## Installation

You can download the latest **setup file** from the [Releases](https://github.com/watermanos/HattrickAnalyzer/releases) page.  
Run the installer and follow the on-screen instructions to install **HattrickAnalyzer** on your computer.  

Once installed, launch the app from your Start Menu or Desktop shortcut.

## Getting Started

### Prerequisites

Make sure you have:

- .NET Framework (compatible with WPF projects)  
- Visual Studio or another C# IDE (optional if using the setup file)  
- A CSV file with player data matching the expected columns  


### How It Works

1. Click **Import CSV** to load player data from a file  
2. The app reads each player and calculates:
   - Current or estimated TSI  
   - Projected TSI after training (approximation)  
   - Training recommendations  
3. You can manually add players with the **Add Player** button  
4. Select one or more players and click **Analyze** to run calculations  
5. Click **Export Excel** to save results in a `.xlsx` file  

## CSV Format Example

```csv
Name,Age,Playmaking,Scoring,Passing,Defending,Winger
John Doe,20,12,10,11,9,8
