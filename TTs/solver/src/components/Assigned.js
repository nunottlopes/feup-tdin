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
import Snackbar from '@material-ui/core/Snackbar';
import { Alert } from "@material-ui/lab"
import { ApiServices } from "../ApiServices";

const { ipcRenderer } = window.require('electron');

function Row(props) {
  const { row } = props;

  return (
    <React.Fragment>
      <TableRow>
        <TableCell component="th" scope="row">
          {row.title}
        </TableCell>
        <TableCell align="center">{row.name}</TableCell>
        <TableCell align="center">{new Date(row.createdAt).toLocaleDateString()}</TableCell>
        <TableCell align="center">{row.status}</TableCell>
        <TableCell align="center">
          <Tooltip title="Open ticket" aria-label="add">
            <IconButton onClick={() => props.setId(row._id)}>
              <LaunchIcon color="primary" />
            </IconButton>
          </Tooltip>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}


const useQuestionStyles = makeStyles({
  divider: {
    margin: '20px 0'
  },
  root: {
    minWidth: 275,
    marginBottom: 15,
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  department: {
    fontSize: 12,
  },
  date: {
    fontSize: 10,
    marginTop: 10,
  },
  cardcontent: {
    "&:last-child": {
      paddingBottom: 16
    }
  },
  response: {
    paddingTop: 7
  }
});

const Question = (props) => {
  const classes = useQuestionStyles();

  const [questions, setQuestions] = useState(null)

  useEffect(() => {
    ApiServices.getSecondayTickets(props.ticket._id, props.ticket.solver).then(response => {
      setQuestions(response.data)
    })
  }, [props.ticket._id, props.ticket.solver, props.update, props.answerEvent])

  return (
    <>
      {questions !== null && questions.length > 0 &&
        <Divider className={classes.divider} />
      }
      {questions !== null && questions.map((question) => (
        <Card key={question._id} className={classes.root}>
          <CardContent className={classes.cardcontent}>
            <Typography className={classes.department} color="textSecondary" gutterBottom>
              Question to department {question.department}
            </Typography>
            <Typography variant="h6" component="h4">
              {question.question}
            </Typography>
            <Typography className={classes.response} variant="body2" component="p">
              {question.response}
            </Typography>
            <Typography className={classes.date} color="textSecondary">
              Last updated: {new Date(question.updatedAt).toUTCString()}
            </Typography>
          </CardContent>
        </Card>
      ))}
    </>
  )
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
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center'
  },
  date: {
    fontSize: 10,
    marginTop: 15,
  }
});

const TicketInfo = (props) => {

  const classes = useTicketStyles();
  const ticket = props.ticket;
  const [response, setResponse] = useState('');

  const handleSecondary = () => {
    ipcRenderer.send('secondary', ticket);
  }

  const solveTicket = () => {
    if (response.trim() === "") return;
    ApiServices.solveTicket(response, ticket._id).then(response => {
      props.setId('')
      props.setSnackbarOpen({
        open: true,
        message: "Ticket solved!",
        severity: "success"
      })
    }).catch(error => {
      // props.setId('')
      props.setSnackbarOpen({
        open: true,
        message: "Unable to solve ticket!",
        severity: "error"
      })
    });
  }

  return (
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
          <Question answerEvent={props.answerEvent} update={props.update} ticket={ticket} />
          <Divider className={classes.divider} />
          <TextField
            variant="outlined"
            fullWidth
            id="response"
            label="Response"
            name="response"
            autoComplete="response"
            value={response}
            onChange={(event) => setResponse(event.target.value)}
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
              <Button variant="outlined" color="primary" onClick={solveTicket}>
                Solve ticket
              </Button>
            </Box>
            <Box t={2} >
              <Button size="small" onClick={handleSecondary}>Create secondary question</Button>
            </Box>
          </div>
        </CardContent>
      </Card>
    </>
  )
}

function Assigned(props) {

  const [id, setId] = useState('')
  const [rows, setRows] = useState([])
  const [snackbarOpen, setSnackbarOpen] = useState({
    open: false,
    message: "",
    severity: "success"
  });

  const [update, setUpdate] = useState(false)

  ipcRenderer.on('secondary:sent', function () {
    setUpdate(!update)
  });

  useEffect(() => {
    ApiServices.getMyTickets(props.name).then(response => {
      response[0].data.sort((a, b) => new Date(a.createdAt) - new Date(b.createdAt))
      response[1].data.sort((a, b) => new Date(a.createdAt) - new Date(b.createdAt))
      response[2].data.sort((a, b) => new Date(a.createdAt) - new Date(b.createdAt))
      setRows([...response[0].data, ...response[1].data, ...response[2].data])
    })
  }, [props.name, id, props.answerEvent])

  return (
    <>
      {id === '' &&
        <>
          <TableContainer component={Paper}>
            <Table aria-label="collapsible table">
              <TableHead>
                <TableRow>
                  <TableCell><Typography style={{ fontWeight: 'bold' }}>Title</Typography></TableCell>
                  <TableCell align="center"><Typography style={{ fontWeight: 'bold' }}>Author</Typography></TableCell>
                  <TableCell align="center"><Typography style={{ fontWeight: 'bold' }}>Date</Typography></TableCell>
                  <TableCell align="center"><Typography style={{ fontWeight: 'bold' }}>Status</Typography></TableCell>
                  <TableCell />
                </TableRow>
              </TableHead>
              <TableBody>
                {rows.map((row) => (
                  <Row key={row._id} row={row} setId={setId} />
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <Snackbar
            open={snackbarOpen.open}
            autoHideDuration={6000}
            onClose={() => setSnackbarOpen({ open: false })}
          >
            <Alert onClose={() => setSnackbarOpen({ open: false })} severity={snackbarOpen.severity}>
              {snackbarOpen.message}
            </Alert>
          </Snackbar>
        </>
      }
      {id !== '' && <TicketInfo ticket={rows.find(x => x._id === id)} answerEvent={props.answerEvent} update={update} setId={setId} setSnackbarOpen={setSnackbarOpen} />}
    </>
  );
}

export default Assigned;
