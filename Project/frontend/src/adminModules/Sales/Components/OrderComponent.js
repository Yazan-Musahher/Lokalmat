import React, { useState } from 'react';
import { FaPen, FaCalendarAlt, FaSearch } from 'react-icons/fa';
import { Button, Col, Row, Container, Form, InputGroup, Dropdown, Tab } from "react-bootstrap";
import '../CSS/OrderComponent.css';
import { useNavigate } from 'react-router-dom';
import TableComponent from '../../Shared/SingleComponents/TableComponent';
import HeaderButtons from "../../Shared/SingleComponents/HeaderButtonsComponent";
import Buttons from "../../Shared/SingleComponents/ButtonsComponent";
import StatusDropdown from "../../Shared/SingleComponents/StatusDropDownCompontent";
import CalendarButton from "../../Shared/SingleComponents/CalendarComponent";
import SearchBar from "../../Shared/SingleComponents/SearchBarComponent";
import orderData from "../TestData/OrderComponentTest";
import financialData from "../TestData/OrderCompFinancialTableTest";
import useSearch from "../../Shared/Hooks/UseSearch";

const OrderComponent = () => {
    const navigate = useNavigate();

    const [items, setItems] = useState(orderData);
    const [financialItems, setFinancialItems] = useState(financialData);
    const [searchQuery, setSearchQuery] = useState('');
    const { query, setQuery, filteredItems } = useSearch(items, ['customer', 'location']);

    // Manage Editing State
    const [isEditing, setIsEditing] = useState(new Set());
    const [editData, setEditData] = useState({});

    const getStatusClass = (status) => {
        if (status === 'Ok') return "status_ok";
        if (status === 'Behandles') return "status_behandles";
        if (status === 'Venter') return "status_venter";
    };

    const getProfitClass = (profit) => {
        const profitValue = parseInt(profit.replace(/[^0-9\-]/g, ''), 10);
        if (profitValue < 0) return 'negative-profit';
        if (profitValue < 20000) return 'low-profit';
        return 'high-profit';
    };

    const toggleItemSelection = (event, id) => {
        event.stopPropagation();
        const updatedItems = items.map(item => {
            if (item.id === id) {
                return {...item, isSelected: !item.isSelected};
            }
            return item;
        });
        setItems(updatedItems);
    };

    const handleEdit = (event, clickedItem) => {
        event.stopPropagation();
        const anyItemSelected = items.some(item => item.isSelected);

        const newEditingIds = new Set();
        const newEditData = {};

        if (anyItemSelected) {
            items.forEach(item => {
                if (item.isSelected) {
                    newEditingIds.add(item.id);
                    newEditData[item.id] = {
                        price: item.price,
                        unit: item.unit,
                        quantity: item.currentQuantity,
                    };
                }
            });
        } else {
            newEditingIds.add(clickedItem.id);
            newEditData[clickedItem.id] = {
                price: clickedItem.price,
                unit: clickedItem.unit,
                quantity: clickedItem.currentQuantity,
            };
        }

        setIsEditing(newEditingIds);
        setEditData(newEditData);
    };

    const handleRowClick = (item) => {
        if (isEditing.size === 0) {
            navigate(`/admin/product-page/${item.id}`);
        }
    };

    const handleNewOrderClick = () => {
        // Use a fixed or dynamically set id here as needed for your application logic
        const defaultId = 'new';  // 'new' could be replaced with a specific id if required
        navigate(`/admin/order/order-page/${defaultId}`);
    };

    const handleSearch = (event) => {
        setQuery(event.target.value);
    };

    const columns = [
        {
            header: '',
            accessor: 'isSelected',
            render: (item) => (
                <input
                    type={"checkbox"}
                    checked={item.isSelected}
                    onChange={(e) => toggleItemSelection(e, item.id)}
                />
            )
        },
        {header: 'Order ID', accessor: 'id'},
        {header: 'Dato', accessor: 'date'},
        {header: 'Kunde', accessor: 'customer'},
        {header: 'Rolle', accessor: 'role'},
        {header: 'Leveransested', accessor: 'location'},
        {header: 'Antall', accessor: 'quantity'},
        {header: 'Status', accessor: 'status',
        render: (item) => (
            <div id="OrderStatusId" className={getStatusClass(item.status)}>
                {item.status}
            </div>
        )
    },
    {
        header: '',
        render: (item) => (
            <button
                className="btn"
                onClick={(e) => {
                    e.stopPropagation(); // Prevent the row click
                    handleEdit(e, item); // Start editing
                }}
            >
                <FaPen />
            </button>
        ),
    },
];

const financialColumns = [
    { header: 'Måned', accessor: 'month' },
    { header: 'Total omsetning', accessor: 'totalSales' },
    { header: 'Solgte vare', accessor: 'soldItems' },
    {
        header: 'Profit', accessor: 'profit',
        render: (item) => (
            <div className={getProfitClass(item.profit)}>
                {item.profit}
            </div>
        )
    },
];

return (
    <Container id="OrderId" className="container bg-white shadow rounded my-4 p-4">
        <HeaderButtons
            title="Ordre"
            buttons={[
                { text: "Slett ordre", variant: "outline-danger", className: "me-2", onClick: () => console.log("Deleting order") },
                { text: "Rediger ordre", variant: "outline-secondary", className: "me-2 btn_botm", onClick: () => console.log("Editing order") },
                { text: "+ Ny ordre", className: "btn_add_new", onClick: handleNewOrderClick }
            ]}
        />

        <Row className="mb-3 gx-3 align-items-center">
            <Col md={6} lg={6}>
                <SearchBar
                    value={query}
                    placeholder="Raskt søk"
                    onChange={handleSearch}
                />
            </Col>
            <Col md={6} lg={6} className="text-end mt-md-0 mt-2">
                <CalendarButton onClick={() => console.log("Calendar clicked")} />

                <StatusDropdown
                    label="Salg"
                    customClass="me-2 mb-2"
                    statuses={['Processed', 'In Processing', 'Done']}
                    onSelect={() => console.log("Status selected")}
                />
                <StatusDropdown
                    label="Status"
                    customClass="me-2 mb-2"
                    statuses={['Processed', 'In Processing', 'Done']}
                    onSelect={() => console.log("Status selected")}
                />
                <StatusDropdown
                    label="Filter"
                    customClass="me-2 mb-2"
                    statuses={['Processed', 'In Processing', 'Done']}
                    onSelect={() => console.log("Status selected")}
                />
            </Col>
        </Row>

        <TableComponent data={filteredItems} columns={columns} onRowClick={handleRowClick} />

        <Row className="mb-3 gx-3 align-items-center">
            <Col md={6} lg={6}>
                <Buttons
                    buttons={[
                        { text: "Eksporter til Excel", className: "mx-2 mb-2 btn_botm", onClick: () => console.log("Exporting to Excel") },
                        { text: "Importer Vare", className: "mx-2 mb-2 btn_botm", onClick: () => console.log("Importing items") }
                    ]}
                />
            </Col>
            <Col md={6} lg={6} className="text-end mt-3 ">
                <TableComponent data={financialItems} columns={financialColumns} className="financialTable" />
            </Col>
        </Row>
    </Container>
);
};

export default OrderComponent;
