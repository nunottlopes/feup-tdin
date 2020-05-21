import React, { useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';

import Questions from "../components/Questions";

const drawerWidth = 240;

const {ipcRenderer} = window.require('electron');

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  drawerContainer: {
    overflow: 'auto',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
}));

function Main() {
  const classes = useStyles();

  // const [name, setName] = useState("")
  // TODO:
  const [name, setName] = useState("casa")

  ipcRenderer.on('login', function(e, item){
    setName(item)
  });

  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar>
          <Typography variant="h6" noWrap>
            Department {name}
          </Typography>
        </Toolbar>
      </AppBar>
      <main className={classes.content}>
        <Toolbar />
        <Questions name={name}/>
      </main>
    </div>
  );
}

export default Main;
