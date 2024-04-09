import React from 'react';
import { InputGroup, Form } from 'react-bootstrap';
import { FaSearch } from 'react-icons/fa';
const SearchBar = ({ value, placeholder, onChange }) => {
  return (
    <InputGroup >
      <InputGroup.Text className="mb-2 search-input-group" >
        <FaSearch/>
      </InputGroup.Text>
      <Form.Control
          
          className= "mb-2 search-input"
        type="search"
        value={value}
        onChange={onChange}
        placeholder={placeholder}
      />
    </InputGroup>
  );
};

export default SearchBar;