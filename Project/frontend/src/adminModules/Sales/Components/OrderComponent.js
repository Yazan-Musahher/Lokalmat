import React, { useState } from 'react';
import {FaPen,FaCalendarAlt,FaSearch} from 'react-icons/fa';
import {Button, Col, Row, Container, Form, InputGroup, Dropdown, Tab} from "react-bootstrap";
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
    const columns = [
        {
            header: '',
            accessor: 'isSelected',
            render: (item) => (
                <input
                    type={"checkbox"}
                    checked={item.isSelected}
                    onChange={(e) => {
                        e.stopPropagation();
                        toggleItemSelection(e, item.id);
                    }}
                />
            )
        },
        {header: 'Order ID', accessor: 'id',},
        {header: 'Dato', accessor: 'date',},
        {header: 'Kunde', accessor: 'customer',},
        {header: 'Rolle', accessor: 'role',},
        {header: 'Leveransested', accessor: 'location',},
        {header: 'Antall', accessor: 'quantity',},
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
                    <FaPen/>
                </button>
            ),
        },
    ];
    const [financialItems, setFinancialItems] = useState(financialData);

    const financialColumns = [

        {
            header: 'Måned',
            accessor: 'month',
        },
        {
            header: 'Total omsetning',
            accessor: 'totalSales',
        },
        {
            header: 'Solgte vare',
            accessor: 'soldItems',
        },
        {
            header: 'Profit',
            accessor: 'profit',
            // Assuming getProfitClass is a function that returns a CSS class based on the profit value
            render: (item) => (
                <div className={getProfitClass(item.profit)}>
                    {item.profit}
                </div>

            )
        },
        
        
        
        
    ];
    
    const [searchQuery, setSearchQuery] = useState('');

    const getStatusClass = (status)=>{
        if (status === 'Ok') return "status_ok";
        if (status === 'Behandles') return "status_behandles";
        if (status === 'Venter') return "status_venter";
        
    }
  

    //Editing
    const [isEditing, setIsEditing] = useState(new Set());
    const [editData, setEditData] = useState({});
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
    const toggleItemSelection = (event, id) => {
        event.stopPropagation();
        const updateItems = items.map(item => {
            if (item.id === id) {
                return {...item, isSelected: !item.isSelected};
            }
            return item;
        });
        setItems(updateItems);

    };

    ///////////////////////////////Search///////////////////////////////
    const { query, setQuery, filteredItems } = useSearch(items, ['customer', 'location']);

    const handleSearch = (event) => {
        setQuery(event.target.value);
    }

    const getProfitClass = (profit) => {
        const profitValue = parseInt(profit.replace(/[^0-9\-]/g, ''), 10);
        if (profitValue < 0) return 'negative-profit';
        if (profitValue < 20000) return 'low-profit';
        return 'high-profit';
    };

    const handleImport =() => {
    };  
    const handleExport =() => {
    };

    const handleNewOrderClick = () => {
        navigate(`/admin/order/order-page/${id}`);
    };
    

    return(
        <Container id="OrderId" className="container bg-white shadow rounded my-4  p-4">
            <HeaderButtons
                title="Ordre"
                buttons={[
                    { text: "Slett -ordre", variant: "outline-danger", className: "me-2 ", onClick: " " },
                    { text: "Rediger ordre", variant: "outline-secondary", className: "me-2 btn_botm", onClick: handleEdit },
                    { text: "+ Ny ordre", className: "btn_add_new", onClick: handleNewOrderClick }
                ]}
            />
          
            <Row className="mb-3 gx-3 align-items-center ">
                <Col md={6} lg={6}>
                    <SearchBar
                        value={query}
                        placeholder="Raskt søk"
                        onChange={handleSearch}
                    />
                </Col>
                <Col md={6} lg={6} className="text-end mt-md-0 mt-2 ">
                    <CalendarButton onClick={""}  />
                    
                    
                    <StatusDropdown
                        label= "Salg"
                        customClass="me-2 mb-2"
                        statuses={['Processed', 'In Processing', 'Done']}
                        onSelect={""}
                    />
                    <StatusDropdown
                        label= "Status"
                        customClass="me-2 mb-2"
                        statuses={['Processed', 'In Processing', 'Done']}
                        onSelect={""}
                    />
                    <StatusDropdown
                        label= "Filter"
                        customClass="me-2 mb-2"
                        statuses={['Processed', 'In Processing', 'Done']}
                        onSelect={""}
                    />
                    
                    
                    
                    
                </Col>
            </Row>

            <TableComponent  data={filteredItems} columns={columns} onRowClick={handleRowClick}/>

            <Row className="mb-3 gx-3 align-items-center ">
                <Col md={6} lg={6}>
            <Buttons
                buttons={[
                    { text: "Eksporter til Excel", className: "mx-2 mb-2 btn_botm", onClick: " " },
                    { text: "Importer Vare", className: "mx-2 mb-2 btn_botm", onClick: " " }
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