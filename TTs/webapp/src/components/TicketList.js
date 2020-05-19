import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
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
    Divider,
    InputAdornment,
    TextField
} from "@material-ui/core";

import {
    KeyboardArrowUp,
    KeyboardArrowDown,
    Search,
} from "@material-ui/icons";

import { getTickets } from "../ApiServices.js";


const useStyles = makeStyles(() => ({
    row: {
        '& > *': {
            borderBottom: 'unset'
        }
    },

    header: {
        fontWeight: 'bold',
        fontSize: 12
    },

    container: {
        margin: "1em 1em",
        padding: "1em"
    },

    search: {
        marginBottom: "2em"
    }

}));

const main = [
    { header: "ID", detail: (data) => data._id },
    { header: "Worker", detail: (data) => data.name },
    { header: "Title", detail: (data) => data.title },
    { header: "Status", detail: (data) => data.status },
    { header: "Created At", detail: (data) => new Date(data.createdAt).toLocaleDateString() }
]

const details = [
    { header: "ID", detail: (data) => data._id },
    { header: "Worker", detail: (data) => `${data.name} (${data.email})` },
    { header: "Status", detail: (data) => data.status },
    { header: "Title", detail: (data) => data.title },
    { header: "Description", detail: (data) => data.description },
    { header: "Solver", detail: (data) => data.solver || "-" },
    { header: "Response", detail: (data) => data.response || "-" },
    { header: "Created At", detail: (data) => new Date(data.createdAt).toLocaleDateString() },
];


const Entry = (props) => {
    const { ticket } = props;
    const [open, setOpen] = React.useState(false);

    const classes = useStyles();

    return (
        <>
            <TableRow key={`_row${ticket.id}`} className={classes.row}>
                <TableCell>
                    <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
                        {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
                    </IconButton>
                </TableCell>
                {main.map(({ detail }) => (
                    <TableCell align="center">{detail(ticket)}</TableCell>
                ))}
            </TableRow>
            <TableRow key={`details${ticket.id}`}>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box margin={1} >
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
    const [filteredTickets, setFilteredTickets] = useState([]);
    const [ticketsLoading, setTicketsLoading] = useState(false);


    useEffect(() => {
        setTicketsLoading(true);
        const fetchData = async () => {
            const tickets = await getTickets();
            setTickets(tickets.data);
            setFilteredTickets(tickets.data);
            setTicketsLoading(false);
        }
        fetchData();
    }, []);

    const handleSearch = (event) => {
        setTicketsLoading(true);
        if (event.target.value === "")
            setFilteredTickets(tickets);
        else
            setFilteredTickets(tickets.filter(t => t.name.toLowerCase().includes(event.target.value.toLowerCase())));
        setTicketsLoading(false);
    }

    return (
        <>
            <Paper square className={classes.container}>
                <TextField
                    label="Search by worker"
                    fullWidth
                    InputProps={{
                        endAdornment: (
                            <InputAdornment>
                                <IconButton>
                                    <Search />
                                </IconButton>
                            </InputAdornment>
                        )
                    }}
                    className={classes.search}
                    onChange={handleSearch}
                />
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            {main.map(({ header, filter }) => (
                                <TableCell align="center">
                                    <Typography className={classes.header}>{header}</Typography>
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    {!ticketsLoading && filteredTickets &&
                        <TableBody>
                            {filteredTickets.map((ticket) => (
                                <Entry key={ticket.id} ticket={ticket} />
                            ))}
                        </TableBody>
                    }
                </Table>
                {ticketsLoading && <LinearProgress />}
            </Paper>
        </>
    );
}

export default TicketList;