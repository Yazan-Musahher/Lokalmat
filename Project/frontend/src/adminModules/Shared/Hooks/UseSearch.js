import { useState, useMemo } from 'react';

const useSearch = (items, searchProperties) => {
    const [query, setQuery] = useState('');

    const filteredItems = useMemo(() => {
        if (!query) return items;
        return items.filter((item) =>
            searchProperties.some((property) =>
                item[property].toLowerCase().includes(query.toLowerCase())
            )
        );
    }, [items, query, searchProperties]);

    return { query, setQuery, filteredItems };
};

export default useSearch;