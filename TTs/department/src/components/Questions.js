import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import KeyboardArrowLeftIcon from '@material-ui/icons/KeyboardArrowLeft';
import Typography from '@material-ui/core/Typography';
import LaunchIcon from '@material-ui/icons/Launch';
import Tooltip from '@material-ui/core/Tooltip';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Divider from '@material-ui/core/Divider';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { ApiServices } from "../ApiServices";

const {ipcRenderer} = window.require('electron');

function Row(props) {
  const { row } = props;

  return (
    <React.Fragment>
      <TableRow>
        <TableCell component="th" scope="row">
          {row.question}
        </TableCell>
        <TableCell align="center">{row.solver}</TableCell>
        <TableCell align="center">{new Date(row.createdAt).toLocaleDateString()}</TableCell>
        <TableCell align="center">{row.status}</TableCell>
        <TableCell align="center">
          <Tooltip title="Open question" aria-label="add">
            <IconButton onClick={() => props.setId(row._id)}>
              <LaunchIcon color="primary" />
            </IconButton>
          </Tooltip>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

const useTicketStyles = makeStyles({
  root: {
    minWidth: 275,
    marginTop: 10
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  title: {
    fontSize: 14,
  },
  pos: {
    fontSize: 12,
    marginTop: 5,
    marginBottom: 16,
  },
  divider: {
    margin: '20px 0'
  },
  resize: {
    fontSize: 14
  },
  submit: {
    display:'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center'
  },
  date: {
    fontSize: 10,
    marginTop: 15,
  }
});

const QuestionInfo = (props) => {

  const classes = useTicketStyles();
  const ticket = props.question.original;
  const question = props.question;
  const [answer, setAnswer] = useState('');

  const answerQuestion = () => {
    ApiServices.solveSecondayQuestion(question._id, answer).then(response => {
      window.localStorage.removeItem(`${props.name}|${question._id}`)
      props.setId('')
    })
    console.log("Answer question")
  }

  return(
    <>
      <IconButton size="small" onClick={() => props.setId('')}>
        <KeyboardArrowLeftIcon />
      </IconButton>
      <Card className={classes.root}>
        <CardContent>
          <Typography className={classes.title} color="textSecondary" gutterBottom>
            {ticket.name}
          </Typography>
          <Typography variant="h5" component="h2">
            {ticket.title}
          </Typography>
          <Typography className={classes.pos} color="textSecondary">
            {ticket.date}
          </Typography>
          <Typography variant="body2" component="p">
            {ticket.description}
          </Typography>
          <Typography className={classes.date} color="textSecondary">
            Submitted on: {new Date(ticket.createdAt).toUTCString()}
          </Typography>
          <Divider className={classes.divider}/>
          <Card key={question._id} className={classes.root}>
            <CardContent className={classes.cardcontent}>
              <Typography className={classes.department} color="textSecondary" gutterBottom>
                Solver {question.solver} question:
              </Typography>
              <Typography variant="h6" component="h4">
                {question.question}
              </Typography>
              <Typography className={classes.response} variant="body2" component="p">
                {question.response}
              </Typography>
              <Typography className={classes.date} color="textSecondary">
              Submitted on: {new Date(question.createdAt).toUTCString()}
              </Typography>
              <Divider className={classes.divider}/>
              <TextField
                variant="outlined"
                fullWidth
                id="answer"
                label="Answer"
                name="answer"
                autoComplete="answer"
                value={answer}
                onChange={(event) => setAnswer(event.target.value)}
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
              <div className={classes.submit}>
                <Box m={2} >
                  <Button variant="outlined" color="primary" onClick={answerQuestion}>
                    Answer question
                  </Button>
                </Box>
              </div>
            </CardContent>
          </Card>
        </CardContent>
      </Card>
    </>
  )
}

function Questions(props) {

  const [id, setId] = useState('')
  const [rows, setRows] = useState([])

  useEffect(() => {
    // window.localStorage.clear()
    let values = []
    const keys = Object.keys(window.localStorage)
    let i = keys.length
    while ( i-- ) {
      const departmentName = keys[i].split("|")[0];
      if(departmentName === props.name)
        values.push(JSON.parse(window.localStorage.getItem(keys[i])));
    }
    setRows(values)
  }, [props.name, id])

  ipcRenderer.on('queue', function(e, item){
    const parsedItem = JSON.parse(item)
    setRows([...rows, parsedItem])
    window.localStorage.setItem(`${props.name}|${parsedItem._id}`, item);
  });

  return (
    <>
      {id === '' &&
        <TableContainer component={Paper}>
          <Table aria-label="collapsible table">
            <TableHead>
              <TableRow>
                <TableCell><Typography style={{ fontWeight: 'bold' }}>Question</Typography></TableCell>
                <TableCell align="center"><Typography style={{ fontWeight: 'bold' }}>Solver</Typography></TableCell>
                <TableCell align="center"><Typography style={{ fontWeight: 'bold' }}>Date</Typography></TableCell>
                <TableCell align="center"><Typography style={{ fontWeight: 'bold' }}>Status</Typography></TableCell>
                <TableCell />
              </TableRow>
            </TableHead>
            <TableBody>
              {rows.map((row) => (
                <Row key={row._id} row={row} setId={setId}/>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      }
      {id !== '' && <QuestionInfo name={props.name} question={rows.find(x => x._id === id)} setId={setId}/>}
    </>
  );
}

export default Questions;
