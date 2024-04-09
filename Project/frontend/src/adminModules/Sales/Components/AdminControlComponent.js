import React, { useState } from 'react';

import { Col, Row, Container} from "react-bootstrap";
import '../CSS/AdminControlComponent.css';
import TableComponent from '../../Shared/SingleComponents/TableComponent';
import systemErrorData from "../TestData/SystemErrorTableAdminTest";
import StatusDropdown from "../../Shared/SingleComponents/StatusDropDownCompontent";
import CalendarButton from "../../Shared/SingleComponents/CalendarComponent";
import SearchBar from "../../Shared/SingleComponents/SearchBarComponent";
import InfoCard from "../../Shared/SingleComponents/CardComponent";
import searchAdminData from "../TestData/SearchInAdmin";
import newProducersData from "../TestData/newProducersTest";
import followUpAdminData from "../TestData/followUpAdminTest";
const AdminControlComponent = () => {
    const [items, setItems] = useState(systemErrorData);
    const columns = [
     
        {header: 'Problem', accessor: 'id',},
        {header: 'Beskrivelse', accessor: 'description',},
        {header: 'Dato', accessor: 'date',},
       
    ];
    const [adminSearch, setAdminSearch] = useState(searchAdminData);
    const searchColumns = [

        {header: 'ID', accessor: 'id',},
        {header: 'Rolle', accessor: 'role',},
        {header: 'Mail', accessor: 'mail',},
        {header: 'Telefon', accessor: 'phone',},
   

    ];
    const [newProducers, setNewProducers] = useState(newProducersData);
    const newProducersColumns = [

        {header: 'ID', accessor: 'id',},
        {header: 'Navn', accessor: 'name',},
        {header: 'Mail', accessor: 'mail',},
        {header: 'Telefon', accessor: 'phone',},
        {header: 'Dato', accessor: 'date',},

    ];
    const [problems, setProblems] = useState(followUpAdminData);
    const followUpAdminColumns =[
        {header: 'ID', accessor: 'id',},
        {header: 'Navn', accessor: 'name',},
        {header: 'Rolle', accessor: 'role',},
        {header: 'Beskrivelse', accessor: 'description',},
        {header: 'Dato', accessor: 'date',},
        
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



    return(
        <Container className="container bg-white shadow rounded my-4  p-4">

            <Row>
                <Col lg={8}>
                    <Row>
                        {/* Three InfoCards */}
                        <Col md={4}>
                            <InfoCard title="Produsenter" count="300" />
                        </Col>
                        <Col md={4}>
                            <InfoCard title="Transportører" count="300" />
                        </Col>
                        <Col md={4}>
                            <InfoCard title="Kunder" count="5000" />
                        </Col>
                    </Row>
            
                <Row className="mb-3 gx-3 align-items-center">
                    <Col md={12} lg={5} xl={6}>
                        <SearchBar
                            value={searchQuery}
                            placeholder="Raskt søk"
                            onChange={handleSearch}
                        />
                    </Col>
                    <Col md={12} lg={7} xl={6} className="text-lg-end mt-md-2 mb-md-2 mt-2">
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
                    <Row>
                    <Col xs={12} lg={12}>
                        <TableComponent data={searchAdminData} columns={searchColumns} />
                    </Col>
                </Row>
                </Row>
                </Col>
            
                <Col lg={4} >
                    <TableComponent title={"Kritiske system varsler: "} data={systemErrorData} columns={columns} className={"systemWarningTable "} />
                </Col>
                </Row>
            
            <Row className="mb-3 gx-3 align-items-center mt-4 mt-lg-0" >
                <Col xs={12} lg={6} className="p-4">
                <TableComponent title={"Nylige produsenter"} data={newProducersData} columns={newProducersColumns} />
            </Col>
                <Col xs={12} lg={6} className="p-4">
                <TableComponent title={"Til oppfølgning (Handling kreves)"} data={followUpAdminData} columns={followUpAdminColumns} />
            </Col>
            </Row>
            
            
        </Container>
    );
};
export default AdminControlComponent;