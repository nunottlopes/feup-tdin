import React, { useState } from "react";
import { makeStyles } from '@material-ui/core/styles';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Box from '@material-ui/core/Box';

const {ipcRenderer} = window.require('electron');

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  container: {
    margin: 20,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  }
}));

function Secondary() {

  const classes = useStyles();

  const [ticket, setTicket] = useState({})
  const [question, setQuestion] = useState("")

  ipcRenderer.on('secondary', function(e, item){
    setTicket({
      _id: item._id,
      title: item.title
      }
    )
  });

  return (
    <div className={classes.container}>
      <h4>Ask Department</h4>
      <TextField
        variant="outlined"
        fullWidth
        id="question"
        label="Question"
        name="question"
        autoComplete="question"
        value={question}
        onChange={(event) => setQuestion(event.target.value)}
        autoFocus
        multiline
        rows={2}
        rowsMax={4}
        InputProps={{
          classes: {
            input: classes.resize,
          },
        }}
      />
      <Box m={2} >
        <Button variant="outlined" color="primary" onClick={() => console.log("Send")}>
          Send
        </Button>
      </Box>
    </div>
  )
}

export default Secondary;
