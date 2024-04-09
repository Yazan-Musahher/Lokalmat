import React from 'react';
import { Button } from 'react-bootstrap';
import "../CSS/ButtonsColorComponent.css";
const Buttons = ({ buttons }) => {
    return (
        <div className="text-center ">
            {buttons.map((button, index) => (
                <Button
                    key={index}
                    className={button.className }
                    onClick={button.onClick}
                >
                    {button.text}
                </Button>
            ))}
        </div>
    );
};

export default Buttons;