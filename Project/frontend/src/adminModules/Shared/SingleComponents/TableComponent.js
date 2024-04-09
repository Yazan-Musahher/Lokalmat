import React from 'react';
import { Table } from 'react-bootstrap';
import "../CSS/TableComponent.css"

const CustomTable = ({title, data, columns, className, onRowClick}) => {
    const handleRowClick = (item) => {
        
        if (onRowClick) onRowClick(item);
        
    };
    
    return (
        <div className={`custom-table ${className}`}>
            {title && <h3 className="table-title p-2">{title}</h3>} 
        <div className={`table-responsive ${className}`} id="tableResponsiveId">

            <Table className="text-center" bordered-bottom hover>
                <thead>
                <tr>
                    {columns.map((column, index) => (
                        <th key={index}>{column.header}</th>
                    ))}
                </tr>
                </thead>


               
                <tbody>
                {data.map((item, rowIndex) => (
                    <tr key={rowIndex}>
                        {columns.map((column, colIndex) => (
                            <td key={colIndex}
                                onClick={() => column.accessor !== 'isSelected' && column.header !== '' ? handleRowClick(item) : null}>
                                {column.render ? column.render(item) : item[column.accessor]}
                            </td>
                        ))}
                    </tr>
                ))}

                </tbody>
                {/* 
                <tbody>
                {data.map((item, rowIndex) => (
                    <tr key={rowIndex} onClick={() => handleRowClick(item)} style={{cursor: 'pointer'}}>
                        {columns.map((column, colIndex) => (
                            <td key={colIndex}>
                                {column.render ? column.render(item) : item[column.accessor]}
                            </td>
                        ))}
                    </tr>
                ))}

                </tbody>
                */}
                
            </Table>
        </div>
        </div>
    );
};
export default CustomTable;