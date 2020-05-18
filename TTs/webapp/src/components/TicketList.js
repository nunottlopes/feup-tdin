import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
    TableContainer,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    Paper,
    IconButton,
    Collapse,
    Box,
    Typography,
    List,
    ListItem,
    ListItemText,
    LinearProgress,
    Divider
} from "@material-ui/core";

import {
    KeyboardArrowUp,
    KeyboardArrowDown
} from "@material-ui/icons";

import { getTickets } from "../ApiServices.js";


const useStyles = makeStyles(() => ({
    row: {
        '& > *': {
            borderBottom: 'unset'
        }
    },
    id: {
        maxWidth: "3em",
        overflow: "hidden",
        textOverflow: "ellipsis"
    }
}));

const details = [
    { header: "ID", detail: (data) => data._id },
    { header: "Worker", detail: (data) => `${data.name} (${data.email})` },
    { header: "Status", detail: (data) => data.status },
    { header: "Title", detail: (data) => data.title },
    { header: "Description", detail: (data) => data.description },
    { header: "Solver", detail: (data) => data.solver || "-" },
    { header: "Response", detail: (data) => data.response || "-" },
    { header: "Created At", detail: (data) => data.createdAt },
];


const Entry = (props) => {
    const { ticket } = props;
    const [open, setOpen] = React.useState(false);

    const classes = useStyles();

    return (
        <>
            <TableRow key={`_row${ticket.id}`} className={classes.root}>
                <TableCell>
                    <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                        {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row" className={classes.id}>
                    {ticket._id}
                </TableCell>
                <TableCell>{ticket.name}</TableCell>
                <TableCell>{ticket.title}</TableCell>
                <TableCell>{ticket.status}</TableCell>
                <TableCell>{ticket.createdAt}</TableCell>
            </TableRow>
            <TableRow key={`details${ticket.id}`}>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box margin={1}>
                            <Typography variant="h6" gutterBottom component="div">
                                Details
                            </Typography>
                            <List>
                                {details.map(({ header, detail }) => (
                                    <>
                                        <Divider component="li" />
                                        <li>
                                            <Typography
                                                className={classes.dividerFullWidth}
                                                color="textSecondary"
                                                display="block"
                                                variant="caption"
                                            >
                                                {header}
                                            </Typography>
                                        </li>
                                        <ListItem>
                                            <ListItemText primary={detail(ticket)} />
                                        </ListItem>
                                    </>
                                ))}
                            </List>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </>
    );
}

const TicketList = () => {
    const classes = useStyles();

    const [tickets, setTickets] = useState([]);
    const [ticketsLoading, setTicketsLoading] = useState(false);


    useEffect(() => {
        setTicketsLoading(true);
        const fetchData = async () => {
            const tickets = await getTickets();
            setTickets(tickets.data);
            setTicketsLoading(false);
        }
        fetchData();
    }, []);

    return (
        <>
            <TableContainer component={Paper}>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>ID</TableCell>
                            <TableCell>Worker</TableCell>
                            <TableCell>Title</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell>Created At</TableCell>
                        </TableRow>
                    </TableHead>
                    {!ticketsLoading && tickets &&
                        <TableBody>
                            {tickets.map((ticket) => (
                                <Entry key={ticket.id} ticket={ticket} />
                            ))}
                        </TableBody>
                    }
                </Table>
                {ticketsLoading && <LinearProgress />}
            </TableContainer>
        </>
    );
}

export default TicketList;