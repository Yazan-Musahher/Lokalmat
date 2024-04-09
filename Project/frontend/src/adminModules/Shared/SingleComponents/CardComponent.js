import {Card, Dropdown} from 'react-bootstrap';
import React, { useState } from 'react';
import "../CSS/CardComponent.css";
import {faUsers} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";

const InfoCard = ({ title, count, iconClass, bgClass }) => {
    const [dropdownTitle, setDropdownTitle] = useState("Jan 2023");
    
    const months = [
        "Jan 2023", "Feb 2023", "Mar 2023",
    ];

    const handleSelect = (eventKey) => {
        setDropdownTitle(eventKey);
    };

    return (
        <Card  className={`info-card  ${bgClass}`}>
            <Card.Body>
               
                <div className="d-flex justify-content-center justify-content-xl-start align-items-center">

                    <div className={`info-icon d-none d-xl-block mr-xl-2 ${iconClass}`}>
                        <FontAwesomeIcon icon={faUsers}/>
                    </div>

                    <div>
                        <div className="info-count">{count}</div>
                        <Card.Title>{title}</Card.Title>

                        <Dropdown className=" mt-2">
                            <Dropdown.Toggle variant="outline-secondary">
                                {dropdownTitle}
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                                {months.map((month, index) => (
                                    <Dropdown.Item key={index} eventKey={month} onSelect={handleSelect}>
                                        {month}
                                    </Dropdown.Item>
                                ))}
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                </div>
                {/* Full-width dropdown */}

            </Card.Body>
        </Card>
    );
};

export default InfoCard;
