import React, { useState } from 'react';
import {FaPen} from 'react-icons/fa';
import {Col, Row, Container} from "react-bootstrap";
import '../CSS/StockComponent.css';
import TableComponent from '../../Shared/SingleComponents/TableComponent';
import HeaderButtons from "../../Shared/SingleComponents/HeaderButtonsComponent";
import Buttons from "../../Shared/SingleComponents/ButtonsComponent";
import StatusDropdown from "../../Shared/SingleComponents/StatusDropDownCompontent";
import CalendarButton from "../../Shared/SingleComponents/CalendarComponent";
import SearchBar from "../../Shared/SingleComponents/SearchBarComponent";
import transporterData from "../TestData/TransporterComponentTest";
import {useNavigate} from "react-router-dom";

const TransporterComponent = () => {

    const navigate = useNavigate();

    const [items, setItems] = useState(transporterData);
    const columns = [
        {
            header: '',
            accessor: 'isSelected',
            render: (item) => (
                <input
                    type={"checkbox"}
                    checked={item.isSelected}
                    onChange={() => toggleItemSelection(item.id)}

                />
            )
        },
        {header: 'Org.Nr.', accessor: 'id',},
        {header: 'Navn', accessor: 'name',},
        {header: 'Mail', accessor: 'email',},
        {header: 'Telefon', accessor: 'phone',},
        {header: 'Område', accessor: 'area',},

      

        {
            header: '',
            render: (item) => (
                <button className="btn" onClick={() => handleEdit(item.id)}><FaPen/></button>

            ),
        },
    ];


    const [searchQuery, setSearchQuery] = useState('');

    const handleSearch = (query) => {
        setSearchQuery(query);

    }
    const handleExport =() => {
    };

    const toggleItemSelection = (id) => {
        const updateItems = items.map(item =>{
            if(item.id === id){
                return {...item, isSelected:!item.isSelected};
            }
            return item;
        });
        setItems(updateItems);

    };
    const handleEdit = (id) =>{
    };
    const handleImport =() => {
    };

    const handleNewTransporterClick = () => {
        navigate('/admin/transporter/transporter-page');
    };

    const handleEditButton = (id) => {
        navigate(`/admin/transporter/transporter-page/${id}`);
    };
    

    return(
        <Container className="container bg-white shadow rounded my-4  p-4">
            <HeaderButtons
                title="Leverandører"
                buttons={[
                    { text: "Slett leverandør", variant: "outline-danger", className: "me-2 ", onClick: " " },
                    { text: "Rediger leverandør", variant: "outline-secondary", className: "me-2  btn_botm", onClick: handleEditButton },
                    { text: "+ Ny leverandør", variant: "success", className: "btn_add_new", onClick: handleNewTransporterClick }
                ]}
            />
            <Row className="mb-3 gx-3 align-items-center">
                <Col md={8} lg={6}>
                    <SearchBar
                        value={searchQuery}
                        placeholder="Raskt søk"
                        onChange={handleSearch}
                    />
                </Col>
                <Col md={4} lg={6} className="text-end mt-md-0 mt-2">
                    <CalendarButton onClick={""}  />
                    <StatusDropdown
                        label={"Status"}
                        statuses={['Processed', 'In Processing', 'Done']}
                        onSelect={""}
                    />
                </Col>
            </Row>

            <TableComponent data={items} columns={columns} />
            <Buttons
                buttons={[
                    { text: "Eksporter til Excel", className: "mx-2 mb-2 btn_botm", onClick: " " },
                    { text: "Importer Leverandør", className: "mx-2 mb-2 btn_botm", onClick: " " }
                ]}
            />

        </Container>
    );
};
export default TransporterComponent;