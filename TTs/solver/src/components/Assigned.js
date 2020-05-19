import React, { useState } from 'react';
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

const {ipcRenderer} = window.require('electron');

function Row(props) {
  const { row } = props;

  return (
    <React.Fragment>
      <TableRow>
        <TableCell component="th" scope="row">
          {row.title}
        </TableCell>
        <TableCell align="center">{row.name}</TableCell>
        <TableCell align="center">{row.date}</TableCell>
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
    margin: '25px 0'
  },
  root: {
    minWidth: 275,
  },
  bullet: {
    display: 'inline-block',
    margin: '0 2px',
    transform: 'scale(0.8)',
  },
  title: {
    fontSize: 12,
  },
  pos: {
    marginBottom: 12,
  },
});

const Question = (props) => {
  const classes = useQuestionStyles();

  const question = {
    question: "What can I do?",
    response: "Delete it all fer gte rjg alg merjg am fgonregerong gerg jeraig agk erig erg arjr giuaer gjaer gier geprgj ar gier gipea "
  }

  return(
    <>
      <Divider className={classes.divider}/>
      <Card className={classes.root}>
        <CardContent>
          <Typography className={classes.title} color="textSecondary" gutterBottom>
            Department Question
          </Typography>
          <Typography variant="h6" component="h4">
            {question.question}
          </Typography>
          {/* <Typography className={classes.pos} color="textSecondary">
            adjective
          </Typography> */}
          <Typography variant="body2" component="p">
            {question.response}
          </Typography>
        </CardContent>
      </Card>
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
    margin: '25px 0'
  },
  resize: {
    fontSize: 14
  },
  submit: {
    display:'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center'
  }
});

const TicketInfo = (props) => {

  const classes = useTicketStyles();
  const ticket = props.ticket;
  const [response, setResponse] = useState('');

  const handleSecondary = () => {
    ipcRenderer.send('secondary', ticket);
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
          <Question/>
          <Divider className={classes.divider}/>
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
              <Button variant="outlined" color="primary" onClick={() => console.log('Solve')}>
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

function Assigned() {

  const [id, setId] = useState('')

  const rows = [
    {_id: '1',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac ffer frferf erg er gerw gw g rtg rtg tw gtwgwrtgrtgrt trg problem', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '2',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '3',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '4',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '5',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'}
  ];

  return (
    <>
      {id === '' &&
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
                <Row key={row._id} row={row} setId={setId}/>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      }
      {id !== '' && <TicketInfo ticket={rows.find(x => x._id === id)} setId={setId}/>}
    </>
  );
}

export default Assigned;
