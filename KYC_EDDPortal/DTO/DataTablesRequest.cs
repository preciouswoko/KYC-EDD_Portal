﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.DTO
{
    public class DataTablesRequest 
    {

        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }
        public List<Column> Columns { get; set; }
        public List<Order> Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public bool isregexvalue { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search1 Search { get; set; }
        public bool IsOrdered { get; set; }
        public int OrderNumber { get; set; }
        public int SortDirection { get; set; }
    }

    public class Search1
    {
        public string Value { get; set; }
        public bool IsRegexValue { get; set; }
    }
    public class DataTablesResponse
    {
        public int draw { get; }
        public IEnumerable data { get; }
        public int recordsTotal { get; }
        public int recordsFiltered { get; }
        public DataTablesResponse(int _draw, IEnumerable _data, int _recordsFiltered, int _recordsTotal)
        {
            draw = _draw;
            data = _data;
            recordsFiltered = _recordsFiltered;
            recordsTotal = _recordsTotal;
        }


    }
}
