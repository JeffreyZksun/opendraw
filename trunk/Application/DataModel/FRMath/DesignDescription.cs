/*
 * Author: Sun Zhongkui
 * History:  
 *  2001-5-10 Create
 * 
 * This file is nothing but a design doc.
 * 
 * The Geometry, matrix and vector classes just define the interface. They hold an adaptor to 
 *  the implementation. They don't hold any other data.Their functions just delegate the operation
 *  to the adaptor class.
 *  
 * The adaptor classes define the same functions as the Geometry, matrix and vector classes.
 *  They don't holde any data. General all the functions defined in it are abstract.
 *  
 * The classes derived from adaptor base are the implementation classes. They hold the actual data.
 *  There exist several kinds of the implementation.
 * 
 * The factory class identify which implementation is used currently.
 * 
 * 
 */