import React from 'react';
import { Dropdown, Form } from 'react-bootstrap';
import "../CSS/DropDownComponent.css"
const StatusDropdown = ({label, statuses, customClass, onSelect }) => {
    return (
        <Dropdown className={`d-inline mb-2  ${customClass}`}>
            <Dropdown.Toggle variant="outline-secondary" id="dropdown-status" className="status_custom mb-2">
                {label}
            </Dropdown.Toggle>

            <Dropdown.Menu className="dropdown_menu_cost">
                <Form>
                    {statuses.map((status, index) => (
                        <Form.Check
                            type="checkbox"
                            id={`status-${index}`}
                            label={status}
                            key={index}
                            onChange={() => onSelect(status)}
                        />
                    ))}
                </Form>
            </Dropdown.Menu>
        </Dropdown>
    );
};

export default StatusDropdown;
