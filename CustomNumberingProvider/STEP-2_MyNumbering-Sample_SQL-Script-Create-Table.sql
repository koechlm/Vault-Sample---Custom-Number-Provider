/* MyNumbering Sample
Step 2 - Add table to database created in Step 1 */

use [MyNumberingDB]
Create table autonumber (id int identity, number as (right('000000'+convert([varchar], [id], 0), (6))), value varchar (10))