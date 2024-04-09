import React, {useRef, useState} from 'react';
import {FaPen} from 'react-icons/fa';
import {Col, Row, Container} from "react-bootstrap";
import '../CSS/StockComponent.css';
import TableComponent from '../../Shared/SingleComponents/TableComponent';
import itemsData from "../TestData/StockComponentTest";
import HeaderButtons from "../../Shared/SingleComponents/HeaderButtonsComponent";
import Buttons from "../../Shared/SingleComponents/ButtonsComponent";
import StatusDropdown from "../../Shared/SingleComponents/StatusDropDownCompontent";
import CalendarButton from "../../Shared/SingleComponents/CalendarComponent";
import SearchBar from "../../Shared/SingleComponents/SearchBarComponent";
import useSearch from "../../Shared/Hooks/UseSearch";
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import exportToExcel from "../../Shared/SingleComponents/ExportExcelComponent";
import {processItemsForExport} from "../../Shared/Utils/ExportUtilis";
import { nb } from 'date-fns/locale';
import {Outlet, useNavigate} from 'react-router-dom';

const StockComponent = () => {
    const [items, setItems] = useState(itemsData);
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

        {header: 'Produkt', accessor: 'product',},
        {
            header: 'Pris',
            accessor: 'price',
            render: (item) =>  isEditing.has(item.id) ? (
                <div style={{ display: 'flex', alignItems: 'center' }}>
                    kr
                    <input
                        type="number"
                        value={editData[item.id]?.price || ''}
                        onChange={(e) => setEditData({ ...editData, [item.id]: { ...editData[item.id], price: e.target.value }})}
                        style={{ maxWidth: '80px', margin: '0 5px' }}
                    />
                    /
                    <input
                        type="text"
                        value={editData[item.id]?.unit || ''}
                        onChange={(e) => setEditData({ ...editData, [item.id]: { ...editData[item.id], unit: e.target.value }})}
                        style={{ maxWidth: '60px', margin: '0 5px' }}
                    />
                </div>
            ) : (
                <span>kr {item.price} /{item.unit}</span>
            )
        },
        {header: 'Kategori', accessor: 'category',},
        {header: 'Leverandør', accessor: 'supplier',},
        {header: 'Instruksjon', accessor: 'instruction',},
        {
            header: 'Mengde',
            accessor: 'quantity',
            render: (item) => {
                if (isEditing.has(item.id)) {
                    return (
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                            <input
                                type="number"
                                value={editData[item.id]?.quantity || ''}
                                onChange={(e) => setEditData({ ...editData, [item.id]: { ...editData[item.id], quantity: e.target.value }})}

                                style={{ maxWidth: '60px', margin: '0 5px' }}
                            />
                            <span>/{item.originalQuantity}</span>
                        </div>
                    );
                } else {
                    return `${item.currentQuantity}/${item.originalQuantity}`;
                }
            }
        },

        {
            header: 'Vurdering',
            accessor: 'rating',
            render: (item) => (
                <div id="VurderingsId" className={getRatingClass(item.rating)}>
                    {item.rating} %
                </div>
            )

        },

        {
            header: '',
            render: (item) => (
                <div>
                    {isEditing.has(item.id) ? (
                        <Buttons
                            buttons={[
                                {
                                    text: "Lagre",
                                    className: "mx-2 mb-2 btn_botm btn-success", // Ensure this matches your desired styling
                                    onClick: () => saveChanges(item.id)
                                }
                            ]}
                        />
                    ) : (

                        <button
                            className="btn"
                            onClick={(e) => {
                                e.stopPropagation(); // Prevent the row click
                                handleEdit(e,item); // Start editing
                            }}
                        >
                            <FaPen/>
                        </button>
                    )}
                </div>
            ),
        },
    ];



///////////////////////////////Search///////////////////////////////
    const { query, setQuery, filteredItems } = useSearch(items, ['product', 'category']);

    const handleSearch = (event) => {
        setQuery(event.target.value);
    }

    ///////////////////////////////Editing///////////////////////////////
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
  
    const navigate = useNavigate();
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

    const saveChanges = (id) => {
        const updatedItems = items.map(item => {
            if (item.id === id) {
                const { price, unit, quantity } = editData[id];
                return { ...item, price, unit, currentQuantity: quantity , isSelected: false};
            }
            return item;
        });
        setItems(updatedItems);

   
        const newEditingIds = new Set(isEditing);
        newEditingIds.delete(id);
        setIsEditing(newEditingIds);

      
        const newEditData = { ...editData };
        delete newEditData[id];
        setEditData(newEditData);
    };



    //Import export
    const handleImport = () => {
    };
    
    const handleExportToExcel = () => {
        const dataToExport = processItemsForExport(items, ['isSelected', 'rating']);
        exportToExcel({ data: dataToExport, filename: 'ItemsData.xlsx' });
    };

    ///////////////////////////////Calender///////////////////////////////
    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(null);
    const [showDatePicker, setShowDatePicker] = useState(false);
    const datePickerRef = useRef();
    const handleCalendarClick = () => {
        // Toggle the display of the DatePicker
        setShowDatePicker(!showDatePicker);
    };
    
    
    //Color based on rating
    const getRatingClass = (rating) => {
        if (rating >= 80) return "rating-high";
        if (rating >= 60) return "rating-medium";
        if (rating < 60) return "rating-low";
        return "";
    };
    const handleNewProductClick = () => {
        navigate('/admin/stock/product-page');
    };

    const handleEditButton = (id) => {
        navigate(`/admin/stock/product-page/${id}`);
    };



    return(

        <Container className="container bg-white shadow rounded my-4  p-4">

            <HeaderButtons
                title="Varelager"
                buttons={[

                    { text: "Slett vare", variant: "outline-danger", className: "me-2 ", onClick: " " },
                    { text: "Rediger vare", variant: "outline-secondary", className: "me-2  btn_botm", onClick: handleEditButton },
                    { text: "+ Ny vare", variant: "success", className: "btn_add_new", onClick: handleNewProductClick }

                ]}
            />

            <Row className="mb-3 gx-3 align-items-center">
                <Col md={8} lg={6}>
                    <SearchBar
                        value={query}
                        placeholder="Raskt søk"
                        onChange={handleSearch}
                    />
                </Col>
                
                    <Col md={4} lg={6} className="text-end mt-md-0 mt-2">

                        <CalendarButton onClick={handleCalendarClick}/>
                        {showDatePicker && (
                            <DatePicker
                                ref={datePickerRef}
                                locale={nb}
                                selectsRange={true}
                                startDate={startDate}
                                classname="date-picker-wrapper"
                                endDate={endDate}
                                onChange={(update) => {
                                    const [start, end] = update;
                                    setStartDate(start);
                                    setEndDate(end);
                                }}
                                withPortal
                                inline
                            />
                        )}

               
                <StatusDropdown
                    label={"Status"}
                    statuses={['Processed', 'In Processing', 'Done']}
                    onSelect={""}
                />
            </Col>
        </Row>

    <TableComponent data={filteredItems} columns={columns} onRowClick={handleRowClick}/>
            <Buttons
                buttons={[
                    {text: "Eksporter til Excel", className: "mt-4 mx-2 mb-2 btn_botm",  onClick: handleExportToExcel},
                    {text: "Importer Vare", className: "mt-4 mx-2 mb-2 btn_botm", onClick: " "}
                ]}
            />


        </Container>
    );
};
export default StockComponent;