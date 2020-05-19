import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Box from '@material-ui/core/Box';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import LaunchIcon from '@material-ui/icons/Launch';
import Tooltip from '@material-ui/core/Tooltip';

const useRowStyles = makeStyles({
  root: {
    '& > *': {
      borderBottom: 'unset',
    },
  },
  email: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center'
  },
  description: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center'
  },
  header: {
    fontWeight:'bold',
    fontSize: 12

  },
  message: {
    fontSize: 12
  },
  table_header: {
    fontWeight:'bold'
  }
});

const openTicket = (id) => {
  console.log('Open ticket page')
}

function Row(props) {
  const { row } = props;
  const [open, setOpen] = React.useState(false);
  const classes = useRowStyles();

  return (
    <React.Fragment>
      <TableRow className={classes.root}>
        <TableCell>
          <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.title}
        </TableCell>
        <TableCell align="center">{row.name}</TableCell>
        <TableCell align="center">{row.date}</TableCell>
        <TableCell align="center">{row.status}</TableCell>
        <TableCell align="center">
          <Tooltip title="Open ticket" aria-label="add">
            <IconButton onClick={() => openTicket(row._id)}>
              <LaunchIcon color="primary" />
            </IconButton>
          </Tooltip>
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box margin={1}>
              <Grid container wrap="nowrap" spacing={2}>
                <Grid item>
                <Typography className={classes.header}>Email:</Typography>
                </Grid>
                <Grid item xs>
                  <Typography className={classes.message}>{row.email}</Typography>
                </Grid>
              </Grid>
              <Grid container wrap="nowrap" spacing={2}>
                <Grid item>
                <Typography className={classes.header}>Description:</Typography>
                </Grid>
                <Grid item xs>
                  <Typography className={classes.message}>{row.description}</Typography>
                </Grid>
              </Grid>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

const useTableStyles = makeStyles({
  header: {
    fontWeight:'bold'
  }
});

function Assigned() {

  const classes = useTableStyles();

  const rows = [
    {_id: '1',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac ffer frferf erg er gerw gw g rtg rtg tw gtwgwrtgrtgrt trg problem', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '2',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgrrenforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '3',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '4',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'},
    {_id: '5',name: 'Antonio', email: 'email@example.com', title: 'Tenho um problema com o meu mac', description: 'renforen rg rtg rrerng rgherg  ewgh wgr gewih giehrw kgerw gewr gkerw gew', status: 'assigned', date: '2016-05-12 22:34'}
  ];

  return (
    <TableContainer component={Paper}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableRow>
            <TableCell />
            <TableCell><Typography className={classes.header}>Title</Typography></TableCell>
            <TableCell align="center"><Typography className={classes.header}>Author</Typography></TableCell>
            <TableCell align="center"><Typography className={classes.header}>Date</Typography></TableCell>
            <TableCell align="center"><Typography className={classes.header}>Status</Typography></TableCell>
            <TableCell />
            <TableCell />
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row) => (
            <Row key={row._id} row={row} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

export default Assigned;
