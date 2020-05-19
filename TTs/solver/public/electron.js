const {app, BrowserWindow, Menu, ipcMain} = require('electron')

const path = require("path");
const isDev = require("electron-is-dev");

let mainWindow;
let loginWindow;

// require("update-electron-app")({
//   repo: "kitze/react-electron-example",
//   updateInterval: "1 hour"
// });

function createMainWindow() {
  mainWindow = new BrowserWindow({
    width: 1000,
    height: 680,
    webPreferences: {
      nodeIntegration: true
    }
  });
  mainWindow.loadURL(
    isDev
      ? "http://localhost:3000?main"
      : `file://${path.join(__dirname, "../build/index.html?main")}`
  );
  mainWindow.on("closed", () => {
    mainWindow = null;
    app.quit();
  });
}

function createLoginWindow() {
  loginWindow = new BrowserWindow({
    width: 500,
    height: 400,
    webPreferences: {
      nodeIntegration: true
    }
  });
  loginWindow.loadURL(
    isDev
      ? "http://localhost:3000?login"
      : `file://${path.join(__dirname, "../build/index.html?login")}`
  );
  loginWindow.on("closed", () => (loginWindow = null));
}

const mainMenuTemplate =  [
  // Each object is a dropdown
  {
    label: 'File',
    submenu:[
      {
        label: 'Quit',
        accelerator:process.platform == 'darwin' ? 'Command+Q' : 'Ctrl+Q',
        click(){
          app.quit();
        }
      }
    ]
  }
];

// Add developer tools option if in dev
if(process.env.NODE_ENV !== 'production'){
  mainMenuTemplate.push({
    label: 'Developer Tools',
    submenu:[
      {
        role: 'reload'
      },
      {
        label: 'Toggle DevTools',
        accelerator:process.platform == 'darwin' ? 'Command+I' : 'Ctrl+I',
        click(item, focusedWindow){
          focusedWindow.toggleDevTools();
        }
      }
    ]
  });
}

// ACTIONS

ipcMain.on('name:login', function(e, item){

  if(mainWindow !== null) createMainWindow()
  else mainWindow.show()

  mainWindow.webContents.once('dom-ready', () => {
    mainWindow.webContents.send('name:login', item);
  });

  loginWindow.close();
});


app.whenReady().then(() => {
  createLoginWindow()

  // Build menu from template
  const mainMenu = Menu.buildFromTemplate(mainMenuTemplate);
  // Insert menu
  Menu.setApplicationMenu(mainMenu);
})

app.on("window-all-closed", () => {
  // On macOS it is common for applications and their menu bar
  // to stay active until the user quits explicitly with Cmd + Q
  if (process.platform !== "darwin") {
    app.quit();
  }
});

app.on("activate", () => {
  if (BrowserWindow.getAllWindows().length === 0) createLoginWindow()
});
