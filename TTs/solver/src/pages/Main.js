import React, { useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';

import Assigned from "../components/Assigned";
import Unassigned from "../components/Unassigned";

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

  const [index, setIndex] = useState(0)
  const [name, setName] = useState("")

  ipcRenderer.on('login', function(e, item){
    setName(item)
  });

  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar position="fixed" className={classes.appBar}>
        <Toolbar>
          <Typography variant="h6" noWrap>
            Hi {name}!
          </Typography>
        </Toolbar>
        <Tabs value={index} onChange={(event, value) => setIndex(value)} aria-label="simple tabs example">
          <Tab label="Unassigned tickets" {... {id: `simple-tab-0`, 'aria-controls': `simple-tabpanel-0`}} />
          <Tab label="My tickets" {... {id: `simple-tab-1`, 'aria-controls': `simple-tabpanel-1`}} />
        </Tabs>
      </AppBar>
      <main className={classes.content}>
        <Toolbar />
        <Tabs />
        {index === 0 && <Unassigned/>}
        {index === 1 && <Assigned/>}
      </main>
    </div>
  );
}

export default Main;
