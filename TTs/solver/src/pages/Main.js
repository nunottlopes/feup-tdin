import React, { useState } from "react";
import { makeStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import Assigned from "../components/Assigned";
import Unassigned from "../components/Unassigned";
import Secondary from "../components/Secondary";

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

  ipcRenderer.on('name:login', function(e, item){
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
      </AppBar>
      <Drawer
        className={classes.drawer}
        variant="permanent"
        classes={{
          paper: classes.drawerPaper,
        }}
      >
        <Toolbar />
        <div className={classes.drawerContainer}>
          <List>
            <ListItem selected={index === 0} button key="Unassigned tickets" onClick={() => setIndex(0)}>
              <ListItemIcon><InboxIcon /></ListItemIcon>
              <ListItemText primary="Unassigned tickets" />
            </ListItem>
            <ListItem selected={index === 1} button key="My tickets" onClick={() =>  setIndex(1)}>
              <ListItemIcon><InboxIcon /></ListItemIcon>
              <ListItemText primary="My tickets" />
            </ListItem>
            <ListItem selected={index === 2}button key="Secondary tickets" onClick={() =>  setIndex(2)}>
              <ListItemIcon><InboxIcon /></ListItemIcon>
              <ListItemText primary="Secondary tickets" />
            </ListItem>
          </List>
        </div>
      </Drawer>
      <main className={classes.content}>
        <Toolbar />
        {index === 0 && <Unassigned/>}
        {index === 1 && <Assigned/>}
        {index === 2 && <Secondary/>}
      </main>
    </div>
  );
}

export default Main;
