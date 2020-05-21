import React, { useState, useEffect } from "react";
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Snackbar from '@material-ui/core/Snackbar';

import Assigned from "../components/Assigned";
import Unassigned from "../components/Unassigned";

import socketIOClient from "socket.io-client";

const drawerWidth = 240;

let socket = null;

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

  const [assignEvent, setAssignEvent] = useState(false);
  const [answerEvent, setAnswerEvent] = useState(false);
  const [createEvent, setCreateEvent] = useState(false);

  const [snackbarOpen, setSnackbarOpen] = useState({
    open: false,
    message: ""
  });

  useEffect(() => {
    socket = socketIOClient("http://localhost:3001");

    ipcRenderer.on('login', function(e, item){
      setName(item)

      socket.on("assign", data => {
        if(data !== item) setAssignEvent((prev) => !prev)
      });

      socket.on("create", data => {
        setCreateEvent((prev) => !prev)
      });

      socket.on("answer", data => {
        if(data === item){
          setAnswerEvent((prev) => !prev)
          setSnackbarOpen({
            open: true,
            message: "A question has been answered"
          })
        }
      });
    });
  }, [])

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
        {socket !== null && index === 0 && <Unassigned createEvent={createEvent} assignEvent={assignEvent} name={name}/>}
        {socket !== null && index === 1 && <Assigned  answerEvent={answerEvent} name={name}/>}
        <Snackbar
          open={snackbarOpen.open}
          autoHideDuration={6000}
          onClose={() => setSnackbarOpen({ open: false })}
          anchorOrigin={{ vertical: "top", horizontal: "right" }}
          message={snackbarOpen.message}
        />
      </main>
    </div>
  );
}

export default Main;
